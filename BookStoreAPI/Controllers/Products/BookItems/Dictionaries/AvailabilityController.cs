using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Products.BookItems.BookItemDictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Products.BookItems.Dictionaries
{
    /// <summary>
    /// Controller for managing availabilites.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilityController : BaseController<Availability>
    {
        /// <summary>
        /// Initializes a new instance of the AvailabilityController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public AvailabilityController(BookStoreContext context) : base(context)
        {
        }

        // GET: api/Availability
        /// <summary>
        /// Gets a list of active availabilities.
        /// </summary>
        /// <returns>List of active availabilities.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Availability>>> GetAvailabilities()
        {
            return await GetAllEntitiesAsync();
        }

        // GET: api/Availability/{id}
        /// <summary>
        /// Gets a availability by its ID.
        /// </summary>
        /// <param name="id">The ID of the availability to retrieve.</param>
        /// <returns>The availability with the specified ID or NotFound if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Availability>> GetAvailability(int id)
        {
            return await GetEntityByIdAsync(id);
        }

        // POST: api/Availability
        /// <summary>
        /// Creates a new availability.
        /// </summary>
        /// <param name="availability">The new availability to create.</param>
        /// <returns>The created availability.</returns>
        [HttpPost]
        public async Task<ActionResult<Availability>> PostAvailability(Availability availability)
        {
            return await CreateEntityAsync(availability);
        }

        // PUT: api/Availability/{id}
        /// <summary>
        /// Updates an existing availability by its ID.
        /// </summary>
        /// <param name="id">The ID of the availability to update.</param>
        /// <param name="updatedAvailability">The updated availability data.</param>
        /// <returns>The updated availability or NotFound if not found.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAvailability(int id, [FromBody] Availability updatedAvailability)
        {
            return await UpdateEntityAsync(id, updatedAvailability);
        }

        // DELETE: api/Availability/{id}
        /// <summary>
        /// Deactivates an existing availability by its ID.
        /// </summary>
        /// <param name="id">The ID of the availability to deactivate.</param>
        /// <returns>NoContent or NotFound if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAvailability(int id)
        {
            return await DeleteEntityAsync(id);
        }
    }
}
