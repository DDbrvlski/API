using BookStoreAPI.BusinessLogic.BookLogic;
using BookStoreData.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Models.Products.Books;
using BookStoreViewModels.ViewModels.Products.Books.Dictionaries;
using BookStoreViewModels.ViewModels.Products.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Controllers.Products.Books
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : CRUDController<Book, BookPostForView, BookForView, BookDetailsForView>
    {
        public BookController(BookStoreContext context) : base(context)
        {
        }


        protected override async Task<ActionResult<BookDetailsForView?>> GetCustomEntityByIdAsync(int id)
        {
            return await BookB.GetBookById(_context, id);
        }

        protected override async Task<ActionResult<IEnumerable<BookForView>>> GetAllEntitiesCustomAsync()
        {
            return await BookB.GetAllBooks(_context);
        }

        protected override async Task<IActionResult> CreateEntityCustomAsync(BookPostForView entity)
        {
            return await BookB.ConvertEntityPostForViewAndSave<BookB>(entity, _context);
        }

        protected override async Task UpdateEntityCustomAsync(Book oldEntity, BookPostForView updatedEntity)
        {
            await BookB.ConvertEntityPostForViewAndUpdate<BookB>(oldEntity, updatedEntity, _context);
        }

        protected override async Task<IActionResult> DeleteEntityCustomAsync(Book entity)
        {
            return await BookB.DeactivateEntityAndSave<BookB>(entity, _context);
        }
    }
}
