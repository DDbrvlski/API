using BookStoreAPI.Data;
using BookStoreAPI.Interfaces;
using BookStoreAPI.ViewModels.Products.Books;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Helpers.BaseBookController
{
    public class CRUDBookController : BaseBookController, IBookDataStore
    {
        public CRUDBookController(BookStoreContext context) : base(context)
        {
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntity(int id)
        {
            return await DeleteEntityAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDetailsForView>>> GetEntities()
        {
            return await GetAllEntitiesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetailsForView>> GetEntity(int id)
        {
            return await GetCustomEntityByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> PostEntity(BookPostForView entity)
        {
            return await CreateEntityAsync(entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntity(int id, [FromBody] BookPostForView entity)
        {
            return await UpdateEntityAsync(id, entity);
        }
    }
}
