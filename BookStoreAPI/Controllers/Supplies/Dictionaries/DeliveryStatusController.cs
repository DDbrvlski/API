using BookStoreData.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Models.Supplies.Dictionaries;
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
