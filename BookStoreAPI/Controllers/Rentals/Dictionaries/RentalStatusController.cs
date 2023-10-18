using BookStoreAPI.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Rentals.Dictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Rentals.Dictionaries
{
    /// <summary>
    /// Controller for managing rental statuses.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RentalStatusController : CRUDController<RentalStatus>
    {
        /// <summary>
        /// Initializes a new instance of the RentalStatusController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public RentalStatusController(BookStoreContext context) : base(context)
        {
        }

    }
}
