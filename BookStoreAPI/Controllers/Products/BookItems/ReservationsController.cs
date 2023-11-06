using BookStoreData.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Models.Products.BookItems;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Products.BookItems
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : CRUDController<Reservations>
    {
        public ReservationsController(BookStoreContext context) : base(context)
        {
        }
    }
}
