using BookStoreAPI.BusinessLogic.BookItemsLogic;
using BookStoreData.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Models.Products.BookItems;
using BookStoreViewModels.ViewModels.Products.BookItems;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static NuGet.Packaging.PackagingConstants;
using BookStoreViewModels.ViewModels.Products.Books.Dictionaries;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using BookStoreAPI.Interfaces.Services;

namespace BookStoreAPI.Controllers.Products.BookItems
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookItemsController : CRUDController<BookItem, BookItemsPostForView, BookItemsForView, BookItemsDetailsForView>
    {
        private readonly IUserService _userService;
        public BookItemsController(BookStoreContext context, IUserService userService) : base(context)
        {
            _userService = userService;
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
            return await BookItemB.BookDetailsAsync(bookItemId, _userService, _context);

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
