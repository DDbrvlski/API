using BookStoreAPI.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreAPI.Models.Accounts.Dictionaries;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Accounts.Dictionaries
{
    /// <summary>
    /// Controller for managing permissions.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : CRUDController<Permission>
    {
        /// <summary>
        /// Initializes a new instance of the PermissionController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public PermissionController(BookStoreContext context) : base(context)
        {
        }

        
    }
}
