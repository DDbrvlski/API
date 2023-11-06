using BookStoreData.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Models.Customers.CustomerDictionaries;
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
