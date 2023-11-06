using BookStoreData.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Models.Accounts.Dictionaries;
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
