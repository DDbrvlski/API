using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Transactions.Dictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace BookStoreAPI.Controllers
{
    /// <summary>
    /// Controller for managing payment methods.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : BaseController<PaymentMethod>
    {
        /// <summary>
        /// Initializes a new instance of the PaymentMethodController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public PaymentMethodController(BookStoreContext context) : base(context)
        {
        }

        // GET: api/PaymentMethods
        /// <summary>
        /// Gets a list of active payment methods.
        /// </summary>
        /// <returns>List of active payment methods.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentMethod>>> GetPaymentMethods()
        {
            return await GetAllEntitiesAsync();
        }

        // GET: api/PaymentMethods/{id}
        /// <summary>
        /// Gets a payment method by its ID.
        /// </summary>
        /// <param name="id">The ID of the payment method to retrieve.</param>
        /// <returns>The payment method with the specified ID or NotFound if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentMethod>> GetPaymentMethod(int id)
        {
            return await GetEntityByIdAsync(id);
        }

        // POST: api/PaymentMethods
        /// <summary>
        /// Creates a new payment method.
        /// </summary>
        /// <param name="paymentMethod">The new payment method to create.</param>
        /// <returns>The created payment method.</returns>
        [HttpPost]
        public async Task<ActionResult<PaymentMethod>> PostPaymentMethod(PaymentMethod paymentMethod)
        {
            return await CreateEntityAsync(paymentMethod);
        }

        // PUT: api/PaymentMethod/{id}
        /// <summary>
        /// Updates an existing payment method by its ID.
        /// </summary>
        /// <param name="id">The ID of the payment method to update.</param>
        /// <param name="updatedPaymentMethod">The updated payment method data.</param>
        /// <returns>The updated payment method or NotFound if not found.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentMethod(int id, [FromBody] PaymentMethod updatedPaymentMethod)
        {
            return await UpdateEntityAsync(id, updatedPaymentMethod);
        }

        // DELETE: api/PaymentMethod/{id}
        /// <summary>
        /// Deactivates an existing payment method by its ID.
        /// </summary>
        /// <param name="id">The ID of the payment method to deactivate.</param>
        /// <returns>NoContent or NotFound if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentMethod(int id)
        {
            return await DeleteEntityAsync(id);
        }
    }
}
