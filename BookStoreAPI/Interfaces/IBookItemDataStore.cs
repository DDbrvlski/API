using BookStoreAPI.ViewModels.Products.BookItems;
using BookStoreAPI.ViewModels.Products.Books;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Interfaces
{
    public interface IBookItemDataStore
    {
        Task<ActionResult<IEnumerable<BookItemsForView>>> GetBookItems();
        Task<ActionResult<BookItemsDetailsForView>> GetBookItem(int id);
        Task<IActionResult> PostBookItem(BookItemsPostForView entity);
        Task<IActionResult> PutBookItem(int id, [FromBody] BookItemsPostForView entity);
        Task<IActionResult> DeleteBookItem(int id);
    }
}
