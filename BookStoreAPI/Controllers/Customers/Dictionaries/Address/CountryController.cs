using BookStoreData.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Models.Customers.AddressDictionaries;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Customers.Dictionaries.Address
{
    /// <summary>
    /// Controller for managing countries.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : CRUDController<Country>
    {
        /// <summary>
        /// Initializes a new instance of the CountryController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public CountryController(BookStoreContext context) : base(context)
        {
        }
    }
}
