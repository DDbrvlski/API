using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Interfaces;
using BookStoreAPI.Models.Accounts.Dictionaries;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Helpers;
using Microsoft.AspNetCore.Http;
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
