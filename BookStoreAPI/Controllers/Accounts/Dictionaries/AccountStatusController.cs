using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Accounts.Dictionaries;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Accounts.Dictionaries
{
    /// <summary>
    /// Controller for managing accounts status.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountStatusController : BaseController<AccountStatus>
    {
        /// <summary>
        /// Initializes a new instance of the AccountStatusController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public AccountStatusController(BookStoreContext context) : base(context)
        {
        }

        // GET: api/AccountStatus
        /// <summary>
        /// Gets a list of active account statuses.
        /// </summary>
        /// <returns>List of active account statuses.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountStatus>>> GetAccountStatuses()
        {
            return await GetAllEntitiesAsync();
        }

        // GET: api/AccountStatus/{id}
        /// <summary>
        /// Gets a account status by its ID.
        /// </summary>
        /// <param name="id">The ID of the account status to retrieve.</param>
        /// <returns>The accountStatus with the specified ID or NotFound if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountStatus>> GetAccountStatus(int id)
        {
            return await GetEntityByIdAsync(id);
        }

        // POST: api/AccountStatus
        /// <summary>
        /// Creates a new account status.
        /// </summary>
        /// <param name="accountStatus">The new account status to create.</param>
        /// <returns>The created account status.</returns>
        [HttpPost]
        public async Task<ActionResult<AccountStatus>> PostAccountStatus(AccountStatus accountStatus)
        {
            return await CreateEntityAsync(accountStatus);
        }

        // PUT: api/AccountStatus/{id}
        /// <summary>
        /// Updates an existing account status by its ID.
        /// </summary>
        /// <param name="id">The ID of the account status to update.</param>
        /// <param name="updatedAccountStatus">The updated account status data.</param>
        /// <returns>The updated account status or NotFound if not found.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountStatus(int id, [FromBody] AccountStatus updatedAccountStatus)
        {
            return await UpdateEntityAsync(id, updatedAccountStatus);
        }

        // DELETE: api/AccountStatus/{id}
        /// <summary>
        /// Deactivates an existing account status by its ID.
        /// </summary>
        /// <param name="id">The ID of the account status to deactivate.</param>
        /// <returns>NoContent or NotFound if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountStatus(int id)
        {
            return await DeleteEntityAsync(id);
        }
    }
}
