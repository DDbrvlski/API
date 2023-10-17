using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Products.BookItems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Products.BookItems
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : CRUDController<Discount>
    {
        public DiscountController(BookStoreContext context) : base(context)
        {
        }
    }
}
