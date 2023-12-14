using BookStoreAPI.BusinessLogic.DiscountCodeLogic;
using BookStoreAPI.BusinessLogic.DiscountLogic;
using BookStoreAPI.Helpers;
using BookStoreAPI.Helpers.BaseBusinessLogic;
using BookStoreAPI.Interfaces.Services;
using BookStoreData.Data;
using BookStoreData.Models.Accounts;
using BookStoreData.Models.Products.BookItems;
using BookStoreViewModels.ViewModels.Products.BookItems;
using BookStoreViewModels.ViewModels.Products.Books.Dictionaries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BookStoreAPI.BusinessLogic.BookItemsLogic
{
    public class BookItemB : BaseBusinessLogic<BookItem, BookItemsPostForView>
    {
        protected override async Task DeactivateAllConnectedEntities(BookItem entity, BookStoreContext context)
        {
            await BookDiscountManager.DeactivateAllDiscounts(entity, context);
            //await BookDiscountCodeManager.DeactivateAllDiscountCodes(entity, context);
        }

        public static async Task<ActionResult<BookItemsDetailsForView?>> GetBookItemById(BookStoreContext context, int id)
        {
            return await context.BookItem
                .Include(x => x.Translator)
                .Include(x => x.Language)
                .Include(x => x.Edition)
                .Include(x => x.FileFormat)
                .Include(x => x.Form)
                .Include(x => x.Availability)
                .Include(x => x.Book)
                .Include(x => x.BookDiscounts)
                    .ThenInclude(x => x.Discount)
                .Select(element => new BookItemsDetailsForView
                {
                    Id = element.Id,
                    TranslatorName = element.Translator.Name + " " + element.Translator.Surname,
                    LanguageName = element.Language.Name,
                    EditionName = element.Edition.Name,
                    FileFormatName = element.FileFormat.Name,
                    FormName = element.Form.Name,
                    AvailabilityName = element.Availability.Name,
                    BookName = element.Book.Title,
                    BruttoPrice = element.NettoPrice * (1 + (decimal)(element.VAT / 100.0f)),
                    NettoPrice = element.NettoPrice,
                    VAT = element.VAT,
                    ISBN = element.ISBN,
                    Pages = element.Pages,
                    PublishingDate = element.PublishingDate,
                    TranslatorID = element.TranslatorID,
                    LanguageID = element.TranslatorID,
                    EditionID = element.EditionID,
                    BookID = element.BookID,
                    FileFormatID = element.FileFormatID,
                    FormID = element.FormID,
                    AvailabilityID = element.AvailabilityID,
                })
                .FirstAsync();
        }
        public static async Task<ActionResult<IEnumerable<BookItemsForView>>> GetBookItems(BookStoreContext context)
        {
            return await context.BookItem
                .Include(x => x.Book)
                .Include(x => x.Form)
                .Where(x => x.IsActive == true)
                .Select(x => new BookItemsForView
                {
                    Id = x.Id,
                    FormName = x.Form.Name,
                    BookTitle = x.Book.Title,
                    ISBN = x.ISBN,
                    BookID = x.BookID,
                    NettoPrice = x.NettoPrice
                })
                .ToListAsync();
        }

        public static async Task<ActionResult<IEnumerable<BookItemsCarouselForView>>> GetBookItemsForCarousel(BookStoreContext context, int formId)
        {
            return await context.BookItem
                .Include(x => x.Book)
                    .ThenInclude(x => x.BookImages)
                    .ThenInclude(x => x.Image)
                .Include(x => x.Form)
                .Where(x => x.IsActive == true && x.FormID == formId)
                .Select(x => new BookItemsCarouselForView
                {
                    Id = x.Id,
                    Title = x.Book.Title,
                    ImageURL = x.Book.BookImages.OrderBy(x => x.Image.Position).FirstOrDefault(y => y.BookID == x.BookID).Image.ImageURL,
                    FormId = x.FormID,
                    FormName = x.Form.Name
                }).Take(25)
                .ToListAsync();
        }

        //public static async Task<ActionResult<IEnumerable<BookItemsWWWStoreForView>>> GetBookItemsForDisplay(BookStoreContext context, int formId, int typeTitle = 0)
        //{
        //    var items = context.BookItem
        //                .Include(x => x.Book)
        //                    .ThenInclude(x => x.BookAuthors)
        //                    .ThenInclude(x => x.Author)
        //                .Include(x => x.Form)
        //                .Include(x => x.Book)
        //                    .ThenInclude(x => x.BookImages)
        //                .Where(x => x.FormID == formId);

        //    if (typeTitle > 0)
        //    {
        //        items = typeTitle switch
        //        {
        //            1 => items.OrderByDescending(x => x.Score).Take(20),
        //            2 => items.OrderByDescending(x => x.Id).Take(20),
        //            3 => items.OrderByDescending(x => x.SoldUnits).Take(20),
        //            _ => items,
        //        };
        //    }
        //    return await items.Select(x => new BookItemsWWWStoreForView
        //    {
        //        Id = x.Id,
        //        Title = x.Book.Title,
        //        FormId = x.FormID,
        //        FormName = x.Form.Name,
        //        Price = x.NettoPrice * (decimal)1.23,
        //        Score = x.Score,
        //        authors = x.Book.BookAuthors.Select(y => new AuthorsForView
        //        {
        //            Id = (int)y.AuthorID,
        //            Name = y.Author.Name,
        //            Surname = y.Author.Surname,
        //        }).ToList(),

        //    }).ToListAsync();

        //}

        public static async Task<ActionResult<IEnumerable<BookItemsWWWStoreForView>>> GetBookItemsForStoreDisplay(BookStoreContext context, BookItemsWWWStoreFiltersForView filters)
        {
            var items = context.BookItem
                        .Include(x => x.Book)
                            .ThenInclude(x => x.BookAuthors)
                            .ThenInclude(x => x.Author)
                        .Include(x => x.Form)
                        .Include(x => x.FileFormat)
                        .Include(x => x.Edition)
                        .Include(x => x.Book)
                            .ThenInclude(x => x.BookImages)
                        .Where(x => x.IsActive == true)
                        .ApplyBookFilters(filters);

            return await items.Select(x => new BookItemsWWWStoreForView
            {
                Id = x.Id,
                ImageURL = x.Book.BookImages.OrderBy(x => x.Image.Position).FirstOrDefault(y => y.BookID == x.BookID).Image.ImageURL,
                Title = x.Book.Title,
                FormId = x.FormID,
                FormName = x.Form.Name,
                FileFormatId = x.FileFormatID,
                FileFormatName = x.FileFormat.Name,
                EditionId = x.EditionID,
                EditionName = x.Edition.Name,
                Price = x.NettoPrice * (1 + ((decimal)x.VAT / 100)),
                Score = x.Score,
                authors = x.Book.BookAuthors.Select(y => new AuthorsForView
                {
                    Id = (int)y.AuthorID,
                    Name = y.Author.Name,
                    Surname = y.Author.Surname,
                }).ToList(),

            }).ToListAsync();
        }

        public async static Task<ActionResult<BookItemWWWDetailsForView>> BookDetailsAsync(int bookItemId, IUserService userService, BookStoreContext _context)
        {
            var user = await userService.GetUserByToken();

            bool isWishlisted = false;

            if (user != null)
            {
                var wishlist = await _context.Wishlist.FirstAsync(x => x.IsActive && x.CustomerID == user.CustomerID);
                isWishlisted = await _context.WishlistItems
                    .AnyAsync(x => x.IsActive && x.WishlistID == wishlist.Id && x.BookItemID == bookItemId);
            }

            var scoreOccurrences = _context.BookItemReview
                .GroupBy(review => review.Score.Value)
                .ToDictionary(group => group.Key, group => group.Count());

            var allScores = Enumerable.Range(1, 5).ToDictionary(score => score, score => 0);

            var scoreValues = allScores
                .Concat(scoreOccurrences)
                .GroupBy(x => x.Key)
                .ToDictionary(
                    group => group.Key,
                    group => group.Sum(pair => pair.Value)
                );

            return await _context.BookItem
                .Include(x => x.Book)
                    .ThenInclude(x => x.BookAuthors)
                    .ThenInclude(x => x.Author)
                .Include(x => x.Book)
                    .ThenInclude(x => x.BookCategories)
                    .ThenInclude(x => x.Category)
                .Include(x => x.Book)
                    .ThenInclude(x => x.BookImages)
                    .ThenInclude(x => x.Image)
                .Include(x => x.Form)
                .Include(x => x.FileFormat)
                .Include(x => x.Edition)
                .Include(x => x.Book)
                    .ThenInclude(x => x.Publisher)
                .Include(x => x.Language)
                .Include(x => x.Book)
                    .ThenInclude(x => x.OriginalLanguage)
                .Include(x => x.Translator)
                .Where(x => x.Id == bookItemId && x.IsActive)
                .Select(x => new BookItemWWWDetailsForView()
                {
                    Id = x.Id,
                    BookTitle = x.Book.Title,
                    BookId = x.BookID,
                    FormName = x.Form.Name,
                    Score = x.Score,
                    Pages = x.Pages,
                    FormId = x.FormID,
                    Price = x.NettoPrice * (1 + ((decimal)x.VAT / 100)),
                    FileFormatName = x.FileFormat.Name,
                    EditionName = x.Edition.Name,
                    PublisherName = x.Book.Publisher.Name,
                    Language = x.Language.Name,
                    OriginalLanguage = x.Book.OriginalLanguage.Name,
                    TranslatorName = x.Translator.Name + " " + x.Translator.Surname,
                    ISBN = x.ISBN,
                    IsWishlisted = isWishlisted,
                    Description = x.Book.Description,
                    ReleaseDate = x.PublishingDate,
                    Authors = x.Book.BookAuthors.Select(y => new AuthorsForView
                    {
                        Id = (int)y.AuthorID,
                        Name = y.Author.Name,
                        Surname = y.Author.Surname,
                    }).ToList(),
                    Categories = x.Book.BookCategories.Select(y => new CategoryForView
                    {
                        Id = (int)y.CategoryID,
                        Name = y.Category.Name
                    }).ToList(),
                    Images = x.Book.BookImages.Select(y => new ImagesForView
                    {
                        Id = (int)y.ImageID,
                        ImageURL = y.Image.ImageURL,
                        Title = y.Image.Title
                    }).ToList(),
                    ScoreValues = scoreValues
                }).FirstAsync();
        }
    }
}
