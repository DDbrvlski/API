using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Rentals.Dictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Rentals.Dictionaries
{
    /// <summary>
    /// Controller for managing rental types.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RentalTypeController : CRUDController<RentalType>
    {
        /// <summary>
        /// Initializes a new instance of the RentalTypeController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public RentalTypeController(BookStoreContext context) : base(context)
        {
        }

    }
}
