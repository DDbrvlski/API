using BookStoreData.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Models.Accounts.Dictionaries;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Accounts.Dictionaries
{
    /// <summary>
    /// Controller for managing accounts status.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountStatusController : CRUDController<AccountStatus>
    {
        /// <summary>
        /// Initializes a new instance of the AccountStatusController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public AccountStatusController(BookStoreContext context) : base(context)
        {
        }

    }
}
