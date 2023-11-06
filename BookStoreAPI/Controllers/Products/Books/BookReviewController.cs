using BookStoreData.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Models.Products.Books;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Products.Books
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookReviewController : CRUDController<BookReview>
    {
        public BookReviewController(BookStoreContext context) : base(context)
        {
        }
    }
}
