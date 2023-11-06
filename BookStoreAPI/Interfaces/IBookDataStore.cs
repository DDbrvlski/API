using BookStoreViewModels.ViewModels.Products.Books;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Interfaces
{
    public interface IBookDataStore
    {
        Task<ActionResult<IEnumerable<BookForView>>> GetBooks();
        Task<ActionResult<BookDetailsForView>> GetBook(int id);
        Task<IActionResult> PostBook(BookPostForView entity);
        Task<IActionResult> PutBook(int id, [FromBody] BookPostForView entity);
        Task<IActionResult> DeleteBook(int id);
    }
}
