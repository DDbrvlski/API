using BookStoreAPI.BusinessLogic.DiscountCodeLogic;
using BookStoreAPI.BusinessLogic.DiscountLogic;
using BookStoreAPI.Controllers.Products.BookItems.Helpers;
using BookStoreAPI.Helpers;
using BookStoreAPI.Helpers.BaseBusinessLogic;
using BookStoreData.Data;
using BookStoreData.Models.Products.BookItems;
using BookStoreData.Models.Products.Books;
using BookStoreViewModels.ViewModels.Products.BookItems;
using BookStoreViewModels.ViewModels.Products.Books.Dictionaries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Globalization;

namespace BookStoreAPI.BusinessLogic.BookItemsLogic
{
    public class BookItemB : BaseBusinessLogic<BookItem, BookItemsPostForView>
    {
        protected override async Task DeactivateAllConnectedEntities(BookItem entity, BookStoreContext context)
        {
            await BookDiscountManager.DeactivateAllDiscounts(entity, context);
            await BookDiscountCodeManager.DeactivateAllDiscountCodes(entity, context);
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
                    ImgURL = x.Book.BookImages.First().Image.ImageURL,
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
                        .Include(x => x.Book)
                            .ThenInclude(x => x.BookImages)
                        .Where(x => x.IsActive == true)
                        .ApplyFilters(filters);

            return await items.Select(x => new BookItemsWWWStoreForView
            {
                Id = x.Id,
                Title = x.Book.Title,
                FormId = x.FormID,
                FormName = x.Form.Name,
                Price = x.NettoPrice * (decimal)1.23,
                Score = x.Score,
                authors = x.Book.BookAuthors.Select(y => new AuthorsForView
                {
                    Id = (int)y.AuthorID,
                    Name = y.Author.Name,
                    Surname = y.Author.Surname,
                }).ToList(),

            }).ToListAsync();
        }
    }
}
