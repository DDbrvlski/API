using BookStoreAPI.Data;
using BookStoreAPI.Interfaces;
using BookStoreAPI.ViewModels.Products.BookItems;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Helpers.BaseBookItemController
{
    public class CRUDBookItemController : BaseBookItemController, IBookItemDataStore
    {
        public CRUDBookItemController(BookStoreContext context) : base(context)
        {
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookItem(int id)
        {
            return await DeleteEntityAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookItemsForView>>> GetBookItems()
        {
            return await GetAllEntitiesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookItemsDetailsForView>> GetBookItem(int id)
        {
            return await GetEntityByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> PostBookItem(BookItemsPostForView entity)
        {
            return await CreateEntityAsync(entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookItem(int id, [FromBody] BookItemsPostForView entity)
        {
            return await UpdateEntityAsync(id, entity);
        }
    }
}
