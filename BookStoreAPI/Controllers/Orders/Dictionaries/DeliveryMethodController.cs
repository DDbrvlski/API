using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Orders.Dictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Orders.Dictionaries
{
    /// <summary>
    /// Controller for managing delivery methods.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryMethodController : BaseController<DeliveryMethod>
    {
        /// <summary>
        /// Initializes a new instance of the DeliveryMethodController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public DeliveryMethodController(BookStoreContext context) : base(context)
        {
        }

        // GET: api/DeliveryMethod
        /// <summary>
        /// Gets a list of active deliveryMethods.
        /// </summary>
        /// <returns>List of active deliveryMethods.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeliveryMethod>>> GetDeliveryMethods()
        {
            return await GetAllEntitiesAsync();
        }

        // GET: api/DeliveryMethod/{id}
        /// <summary>
        /// Gets a deliveryMethod by its ID.
        /// </summary>
        /// <param name="id">The ID of the deliveryMethod to retrieve.</param>
        /// <returns>The deliveryMethod with the specified ID or NotFound if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<DeliveryMethod>> GetDeliveryMethod(int id)
        {
            return await GetEntityByIdAsync(id);
        }

        // POST: api/DeliveryMethod
        /// <summary>
        /// Creates a new deliveryMethod.
        /// </summary>
        /// <param name="deliveryMethod">The new deliveryMethod to create.</param>
        /// <returns>The created deliveryMethod.</returns>
        [HttpPost]
        public async Task<ActionResult<DeliveryMethod>> PostDeliveryMethod(DeliveryMethod deliveryMethod)
        {
            return await CreateEntityAsync(deliveryMethod);
        }

        // PUT: api/DeliveryMethod/{id}
        /// <summary>
        /// Updates an existing deliveryMethod by its ID.
        /// </summary>
        /// <param name="id">The ID of the deliveryMethod to update.</param>
        /// <param name="updatedDeliveryMethod">The updated deliveryMethod data.</param>
        /// <returns>The updated deliveryMethod or NotFound if not found.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeliveryMethod(int id, [FromBody] DeliveryMethod updatedDeliveryMethod)
        {
            return await UpdateEntityAsync(id, updatedDeliveryMethod);
        }

        // DELETE: api/DeliveryMethod/{id}
        /// <summary>
        /// Deactivates an existing deliveryMethod by its ID.
        /// </summary>
        /// <param name="id">The ID of the deliveryMethod to deactivate.</param>
        /// <returns>NoContent or NotFound if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeliveryMethod(int id)
        {
            return await DeleteEntityAsync(id);
        }
    }
}
