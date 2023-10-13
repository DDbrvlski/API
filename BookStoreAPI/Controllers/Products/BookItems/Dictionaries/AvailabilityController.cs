using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Products.BookItems.BookItemDictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Products.BookItems.Dictionaries
{
    /// <summary>
    /// Controller for managing availabilites.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilityController : CRUDController<Availability>
    {
        /// <summary>
        /// Initializes a new instance of the AvailabilityController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public AvailabilityController(BookStoreContext context) : base(context)
        {
        }

    }
}
