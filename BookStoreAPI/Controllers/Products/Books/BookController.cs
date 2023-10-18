using BookStoreAPI.Data;
using BookStoreAPI.Helpers.BaseBookController;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Products.Books
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : CRUDBookController
    {
        public BookController(BookStoreContext context) : base(context)
        {
        }

    }
}
