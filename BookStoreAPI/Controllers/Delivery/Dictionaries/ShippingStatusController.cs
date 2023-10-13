using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Delivery.Dictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Delivery.Dictionaries
{
    /// <summary>
    /// Controller for managing shipping statuses.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingStatusController : CRUDController<ShippingStatus>
    {
        /// <summary>
        /// Initializes a new instance of the ShippingStatusController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ShippingStatusController(BookStoreContext context) : base(context)
        {
        }
        
    }
}
