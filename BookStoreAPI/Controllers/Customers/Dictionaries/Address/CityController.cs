using BookStoreAPI.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreAPI.Models.Customers.AddressDictionaries;
using BookStoreAPI.Models.Transactions.Dictionaries;
using Microsoft.AspNetCore.Http;
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
