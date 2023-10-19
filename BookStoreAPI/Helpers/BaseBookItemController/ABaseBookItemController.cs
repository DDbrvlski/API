using BookStoreAPI.Models.Products.BookItems;
using BookStoreAPI.Models.Products.Books;
using BookStoreAPI.ViewModels.Products.BookItems;
using BookStoreAPI.ViewModels.Products.Books;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Helpers.BaseBookItemController
{
    public abstract class ABaseBookItemController : ControllerBase
    {
        protected abstract Task<ActionResult<IEnumerable<BookItemsForView>>> GetAllEntitiesAsync();
        protected abstract Task<ActionResult<BookItemsDetailsForView>> GetEntityByIdAsync(int id);
        protected abstract Task<IActionResult> CreateEntityAsync(BookItemsPostForView entity);
        protected abstract Task<IActionResult> UpdateEntityAsync(int id, BookItemsPostForView updatedEntity);
        protected abstract Task<IActionResult> DeleteEntityAsync(int id);
        protected abstract bool EntityExists(int id);

        protected virtual async Task<BookItem?> GetEntityAsync(int id) { return null; }
        protected virtual async Task<BookItemsDetailsForView?> GetCustomEntityByIdAsync(int id) { return null; }
        protected virtual async Task<ActionResult<IEnumerable<BookItemsForView>>> GetAllEntitiesCustomAsync() { return null; }
        protected virtual async Task<IActionResult> CreateEntityCustomAsync(BookItemsPostForView entity) { return null; }
        protected virtual async Task UpdateEntityCustomAsync(BookItem oldEntity, BookItemsPostForView updatedEntity) { }
        protected virtual async Task<IActionResult> DeleteEntityCustomAsync(BookItem entity) { return null; }
    }
}
