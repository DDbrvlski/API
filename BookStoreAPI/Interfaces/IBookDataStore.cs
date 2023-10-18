using BookStoreAPI.ViewModels.Products.Books;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Interfaces
{
    public interface IBookDataStore
    {
        Task<ActionResult<IEnumerable<BookDetailsForView>>> GetEntities();
        Task<ActionResult<BookDetailsForView>> GetEntity(int id);
        Task<IActionResult> PostEntity(BookPostForView entity);
        Task<IActionResult> PutEntity(int id, [FromBody] BookPostForView entity);
        Task<IActionResult> DeleteEntity(int id);
    }
}
