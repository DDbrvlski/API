using BookStoreData.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Models.Orders.Dictionaries;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Orders.Dictionaries
{
    /// <summary>
    /// Controller for managing order statuses.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusController : CRUDController<OrderStatus>
    {
        /// <summary>
        /// Initializes a new instance of the OrderStatusController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public OrderStatusController(BookStoreContext context) : base(context)
        {
        }

    }
}
