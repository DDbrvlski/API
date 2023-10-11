using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Transactions.Dictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Transactions.Dictionaries
{
    /// <summary>
    /// Controller for managing transaction status.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsStatusController : BaseController<TransactionsStatus>
    {
        /// <summary>
        /// Initializes a new instance of the TransactionsStatusController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public TransactionsStatusController(BookStoreContext context) : base(context)
        {
        }

        // GET: api/TransactionsStatus
        /// <summary>
        /// Gets a list of active transactions status.
        /// </summary>
        /// <returns>List of active transaction status.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionsStatus>>> GetTransactionsStatus()
        {
            return await GetAllEntitiesAsync();
        }

        // GET: api/TransactionsStatus/{id}
        /// <summary>
        /// Gets a transaction status by its ID.
        /// </summary>
        /// <param name="id">The ID of the transaction status to retrieve.</param>
        /// <returns>The transaction status with the specified ID or NotFound if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionsStatus>> GetTransactionStatus(int id)
        {
            return await GetEntityByIdAsync(id);
        }

        // POST: api/TransactionsStatus
        /// <summary>
        /// Creates a new transaction status.
        /// </summary>
        /// <param name="transactionStatus">The new transaction status to create.</param>
        /// <returns>The created transaction status.</returns>
        [HttpPost]
        public async Task<ActionResult<TransactionsStatus>> PostTransactionStatus(TransactionsStatus transactionStatus)
        {
            return await CreateEntityAsync(transactionStatus);
        }

        // PUT: api/TransactionsStatus/{id}
        /// <summary>
        /// Updates an existing transaction status by its ID.
        /// </summary>
        /// <param name="id">The ID of the transaction status to update.</param>
        /// <param name="updatedTransactionStatus">The updated transaction status data.</param>
        /// <returns>The updated transaction status or NotFound if not found.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransactionStatus(int id, [FromBody] TransactionsStatus updatedTransactionStatus)
        {
            return await UpdateEntityAsync(id, updatedTransactionStatus);
        }

        // DELETE: api/TransactionsStatus/{id}
        /// <summary>
        /// Deactivates an existing transaction status by its ID.
        /// </summary>
        /// <param name="id">The ID of the transaction status to deactivate.</param>
        /// <returns>NoContent or NotFound if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransactionStatus(int id)
        {
            return await DeleteEntityAsync(id);
        }
    }
}
