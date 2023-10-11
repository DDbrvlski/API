using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Delivery.Dictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Delivery.Dictionaries
{
    /// <summary>
    /// Controller for managing shipping statuses.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingStatusController : BaseController<ShippingStatus>
    {
        /// <summary>
        /// Initializes a new instance of the ShippingStatusController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ShippingStatusController(BookStoreContext context) : base(context)
        {
        }

        // GET: api/ShippingStatus
        /// <summary>
        /// Gets a list of active shipping statuses.
        /// </summary>
        /// <returns>List of active shipping statuses.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShippingStatus>>> GetShippingStatuses()
        {
            return await GetAllEntitiesAsync();
        }

        // GET: api/ShippingStatus/{id}
        /// <summary>
        /// Gets a shipping status by its ID.
        /// </summary>
        /// <param name="id">The ID of the shipping status to retrieve.</param>
        /// <returns>The shipping status with the specified ID or NotFound if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ShippingStatus>> GetShippingStatus(int id)
        {
            return await GetEntityByIdAsync(id);
        }

        // POST: api/ShippingStatus
        /// <summary>
        /// Creates a new shipping status.
        /// </summary>
        /// <param name="shippingStatus">The new shipping status to create.</param>
        /// <returns>The created shipping status.</returns>
        [HttpPost]
        public async Task<ActionResult<ShippingStatus>> PostShippingStatus(ShippingStatus shippingStatus)
        {
            return await CreateEntityAsync(shippingStatus);
        }

        // PUT: api/ShippingStatus/{id}
        /// <summary>
        /// Updates an existing shipping status by its ID.
        /// </summary>
        /// <param name="id">The ID of the shipping status to update.</param>
        /// <param name="updatedShippingStatus">The updated shipping status data.</param>
        /// <returns>The updated shipping status or NotFound if not found.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShippingStatus(int id, [FromBody] ShippingStatus updatedShippingStatus)
        {
            return await UpdateEntityAsync(id, updatedShippingStatus);
        }

        // DELETE: api/ShippingStatus/{id}
        /// <summary>
        /// Deactivates an existing shipping status by its ID.
        /// </summary>
        /// <param name="id">The ID of the shipping status to deactivate.</param>
        /// <returns>NoContent or NotFound if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShippingStatus(int id)
        {
            return await DeleteEntityAsync(id);
        }
    }
}
