using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Orders.Dictionaries;
using Microsoft.AspNetCore.Http;
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
