using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Supplies.Dictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Supplies.Dictionaries
{
    /// <summary>
    /// Controller for managing delivery statuses.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryStatusController : BaseController<DeliveryStatus>
    {
        /// <summary>
        /// Initializes a new instance of the DeliveryStatusController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public DeliveryStatusController(BookStoreContext context) : base(context)
        {
        }

        // GET: api/DeliveryStatus
        /// <summary>
        /// Gets a list of active delivery statuses.
        /// </summary>
        /// <returns>List of active delivery statuses.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeliveryStatus>>> GetDeliveryStatuss()
        {
            return await GetAllEntitiesAsync();
        }

        // GET: api/DeliveryStatus/{id}
        /// <summary>
        /// Gets a delivery status by its ID.
        /// </summary>
        /// <param name="id">The ID of the delivery status to retrieve.</param>
        /// <returns>The delivery status with the specified ID or NotFound if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<DeliveryStatus>> GetDeliveryStatus(int id)
        {
            return await GetEntityByIdAsync(id);
        }

        // POST: api/DeliveryStatus
        /// <summary>
        /// Creates a new delivery status.
        /// </summary>
        /// <param name="deliveryStatus">The new delivery status to create.</param>
        /// <returns>The created delivery status.</returns>
        [HttpPost]
        public async Task<ActionResult<DeliveryStatus>> PostDeliveryStatus(DeliveryStatus deliveryStatus)
        {
            return await CreateEntityAsync(deliveryStatus);
        }

        // PUT: api/DeliveryStatus/{id}
        /// <summary>
        /// Updates an existing delivery status by its ID.
        /// </summary>
        /// <param name="id">The ID of the delivery status to update.</param>
        /// <param name="updatedDeliveryStatus">The updated deliveryStatus data.</param>
        /// <returns>The updated deliveryStatus or NotFound if not found.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeliveryStatus(int id, [FromBody] DeliveryStatus updatedDeliveryStatus)
        {
            return await UpdateEntityAsync(id, updatedDeliveryStatus);
        }

        // DELETE: api/DeliveryStatus/{id}
        /// <summary>
        /// Deactivates an existing delivery status by its ID.
        /// </summary>
        /// <param name="id">The ID of the delivery status to deactivate.</param>
        /// <returns>NoContent or NotFound if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeliveryStatus(int id)
        {
            return await DeleteEntityAsync(id);
        }
    }
}
