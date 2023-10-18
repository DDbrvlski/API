using BookStoreAPI.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Supplies.Dictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Supplies.Dictionaries
{
    /// <summary>
    /// Controller for managing delivery statuses.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryStatusController : CRUDController<DeliveryStatus>
    {
        /// <summary>
        /// Initializes a new instance of the DeliveryStatusController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public DeliveryStatusController(BookStoreContext context) : base(context)
        {
        }

    }
}
