using BookStoreAPI.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreAPI.Models.Products.BookItems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Products.BookItems
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockAmountController : CRUDController<StockAmount>
    {
        public StockAmountController(BookStoreContext context) : base(context)
        {
        }
    }
}
