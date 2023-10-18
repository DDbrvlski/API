using BookStoreAPI.Models.Products.Books;
using BookStoreAPI.ViewModels.Products.Books;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Helpers.BaseBookController
{
    public abstract class ABaseBookController : ControllerBase
    {
        protected abstract Task<ActionResult<IEnumerable<BookDetailsForView>>> GetAllEntitiesAsync();
        protected abstract Task<IActionResult> CreateEntityAsync(BookPostForView entity);
        protected abstract Task<IActionResult> UpdateEntityAsync(int id, BookPostForView updatedEntity);
        protected abstract Task<IActionResult> DeleteEntityAsync(int id);
        protected abstract bool EntityExists(int id);

        protected virtual async Task<Book?> GetEntityByIdAsync(int id) { return null; }
        protected virtual async Task<BookDetailsForView?> GetCustomEntityByIdAsync(int id) { return null; }
        protected virtual async Task<ActionResult<IEnumerable<BookDetailsForView>>> GetAllEntitiesCustomAsync() { return null; }
        protected virtual async Task<IActionResult> CreateEntityCustomAsync(BookPostForView entity) { return null; }
        protected virtual async Task UpdateEntityCustomAsync(Book oldEntity, BookPostForView updatedEntity) { }
        protected virtual async Task<IActionResult> DeleteEntityCustomAsync(Book entity) { return null; }
    }
}
