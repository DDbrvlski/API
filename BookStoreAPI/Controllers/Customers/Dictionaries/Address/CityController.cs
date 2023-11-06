using BookStoreData.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Models.Customers.AddressDictionaries;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Customers.Dictionaries.Address
{
    /// <summary>
    /// Controller for managing cities.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : CRUDController<City>
    {
        /// <summary>
        /// Initializes a new instance of the CityController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public CityController(BookStoreContext context) : base(context)
        {
        }

    }
}
