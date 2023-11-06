using BookStoreData.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Models.Products.BookItems.BookItemDictionaries;
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
