using BookStoreAPI.BusinessLogic.BookItemsLogic;
using BookStoreData.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Models.Products.BookItems;
using BookStoreViewModels.ViewModels.Products.BookItems;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static NuGet.Packaging.PackagingConstants;
using BookStoreViewModels.ViewModels.Products.Books.Dictionaries;

namespace BookStoreAPI.Controllers.Products.BookItems
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookItemsController : CRUDController<BookItem, BookItemsPostForView, BookItemsForView, BookItemsDetailsForView>
    {
        public BookItemsController(BookStoreContext context) : base(context)
        {
        }

        [HttpGet]
        [Route("All-Books")]
        public async Task<ActionResult<IEnumerable<BookItemsWWWStoreForView>>> AllBooks([FromQuery] BookItemsWWWStoreFiltersForView? filters = null)
        {
            return await BookItemB.GetBookItemsForStoreDisplay(_context, filters);
        }

        [HttpGet]
        [Route("Infinite-Carousel-Books")]
        public async Task<ActionResult<IEnumerable<BookItemsCarouselForView>>> InfiniteCarouselBooks()
        {
            return await BookItemB.GetBookItemsForCarousel(_context, 1);
        }

        [HttpGet]
        [Route("Book-Details")]
        public async Task<ActionResult<BookItemWWWDetailsForView>> BookDetails(int bookItemId)
        {
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
                    FormName = x.Form.Name,
                    Score = x.Score,
                    Price = x.NettoPrice * (1 + ((decimal)x.VAT / 100)),
                    FileFormatName = x.FileFormat.Name,
                    EditionName = x.Edition.Name,
                    PublisherName = x.Book.Publisher.Name,
                    Language = x.Language.Name,
                    OriginalLanguage = x.Book.OriginalLanguage.Name,
                    TranslatorName = x.Translator.Name,
                    ISBN = x.ISBN,
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

        //[HttpGet]
        //[Route("Most-Popular-Books")]
        //public async Task<ActionResult<IEnumerable<BookItemsWWWStoreForView>>> MostPopularBooks()
        //{
        //    return await BookItemB.GetBookItemsForStoreDisplay(_context, filters);
        //}

        //[HttpGet]
        //[Route("Recently-Added-Books")]
        //public async Task<ActionResult<IEnumerable<BookItemsWWWStoreForView>>> RecentlyAddedBooks()
        //{
        //    return await BookItemB.GetBookItemsForStoreDisplay(_context, filters);
        //}

        //[HttpGet]
        //[Route("Most-Purchased-Books")]
        //public async Task<ActionResult<IEnumerable<BookItemsWWWStoreForView>>> MostPurchasedBooks()
        //{
        //    return await BookItemB.GetBookItemsForStoreDisplay(_context, filters);
        //}

        //[HttpGet]
        //[Route("All-EBooks")]
        //public async Task<ActionResult<IEnumerable<BookItemsWWWStoreForView>>> AllEBooks()
        //{
        //    return await BookItemB.GetBookItemsForStoreDisplay(_context, filters);
        //}

        [HttpGet]
        [Route("Infinite-Carousel-EBooks")]
        public async Task<ActionResult<IEnumerable<BookItemsCarouselForView>>> InfiniteCarouselEBooks()
        {
            return await BookItemB.GetBookItemsForCarousel(_context, 2);
        }

        //[HttpGet]
        //[Route("Most-Popular-EBooks")]
        //public async Task<ActionResult<IEnumerable<BookItemsWWWStoreForView>>> MostPopularEBooks()
        //{
        //    return await BookItemB.GetBookItemsForStoreDisplay(_context, filters);
        //}

        //[HttpGet]
        //[Route("Recently-Added-EBooks")]
        //public async Task<ActionResult<IEnumerable<BookItemsWWWStoreForView>>> RecentlyAddedEBooks()
        //{
        //    return await BookItemB.GetBookItemsForStoreDisplay(_context, filters);
        //}

        //[HttpGet]
        //[Route("Most-Purchased-EBooks")]
        //public async Task<ActionResult<IEnumerable<BookItemsWWWStoreForView>>> MostPurchasedEBooks()
        //{
        //    return await BookItemB.GetBookItemsForStoreDisplay(_context, filters);
        //}

        protected override async Task<ActionResult<BookItemsDetailsForView?>> GetCustomEntityByIdAsync(int id)
        {
            return await BookItemB.GetBookItemById(_context, id);
        }
        protected override async Task<ActionResult<IEnumerable<BookItemsForView>>> GetAllEntitiesCustomAsync()
        {
            return await BookItemB.GetBookItems(_context);
        }
        protected override async Task<IActionResult> CreateEntityCustomAsync(BookItemsPostForView entity)
        {
            return await BookItemB.ConvertEntityPostForViewAndSave<BookItemB>(entity, _context);
        }
        protected override async Task UpdateEntityCustomAsync(BookItem oldEntity, BookItemsPostForView updatedEntity)
        {
            await BookItemB.ConvertEntityPostForViewAndUpdate<BookItemB>(oldEntity, updatedEntity, _context);
        }
        protected override async Task<IActionResult> DeleteEntityCustomAsync(BookItem entity)
        {
            return await BookItemB.DeactivateEntityAndSave<BookItemB>(entity, _context);
        }
    }
}
