using BookStoreAPI.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Transactions.Dictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Customers.Dictionaries.Customer
{
    /// <summary>
    /// Controller for managing genders.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : CRUDController<Gender>
    {
        /// <summary>
        /// Initializes a new instance of the GenderController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public GenderController(BookStoreContext context) : base(context)
        {
        }
    }
}
