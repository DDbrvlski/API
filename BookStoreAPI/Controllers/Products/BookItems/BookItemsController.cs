using BookStoreAPI.Data;
using BookStoreAPI.Helpers.BaseBookItemController;
using BookStoreAPI.Helpers.BaseController;
using BookStoreAPI.Models.Products.BookItems;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Products.BookItems
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookItemsController : CRUDController<BookItem>
    {
        public BookItemsController(BookStoreContext context) : base(context)
        {
        }
    }
}
