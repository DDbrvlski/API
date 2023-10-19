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
        public async Task<IActionResult> DeleteBook(int id)
        {
            return await DeleteEntityAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookForView>>> GetBooks()
        {
            return await GetAllEntitiesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetailsForView>> GetBook(int id)
        {
            return await GetCustomEntityByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> PostBook(BookPostForView entity)
        {
            return await CreateEntityAsync(entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, [FromBody] BookPostForView entity)
        {
            return await UpdateEntityAsync(id, entity);
        }
    }
}
