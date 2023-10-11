using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Rentals.Dictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Rentals.Dictionaries
{
    /// <summary>
    /// Controller for managing rental types.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RentalTypeController : BaseController<RentalType>
    {
        /// <summary>
        /// Initializes a new instance of the RentalTypeController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public RentalTypeController(BookStoreContext context) : base(context)
        {
        }

        // GET: api/RentalType
        /// <summary>
        /// Gets a list of active rentalTypes.
        /// </summary>
        /// <returns>List of active rentalTypes.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentalType>>> GetRentalTypes()
        {
            return await GetAllEntitiesAsync();
        }

        // GET: api/RentalType/{id}
        /// <summary>
        /// Gets a rentalType by its ID.
        /// </summary>
        /// <param name="id">The ID of the rentalType to retrieve.</param>
        /// <returns>The rentalType with the specified ID or NotFound if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<RentalType>> GetRentalType(int id)
        {
            return await GetEntityByIdAsync(id);
        }

        // POST: api/RentalType
        /// <summary>
        /// Creates a new rentalType.
        /// </summary>
        /// <param name="rentalType">The new rentalType to create.</param>
        /// <returns>The created rentalType.</returns>
        [HttpPost]
        public async Task<ActionResult<RentalType>> PostRentalType(RentalType rentalType)
        {
            return await CreateEntityAsync(rentalType);
        }

        // PUT: api/RentalType/{id}
        /// <summary>
        /// Updates an existing rentalType by its ID.
        /// </summary>
        /// <param name="id">The ID of the rentalType to update.</param>
        /// <param name="updatedRentalType">The updated rentalType data.</param>
        /// <returns>The updated rentalType or NotFound if not found.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRentalType(int id, [FromBody] RentalType updatedRentalType)
        {
            return await UpdateEntityAsync(id, updatedRentalType);
        }

        // DELETE: api/RentalType/{id}
        /// <summary>
        /// Deactivates an existing rentalType by its ID.
        /// </summary>
        /// <param name="id">The ID of the rentalType to deactivate.</param>
        /// <returns>NoContent or NotFound if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRentalType(int id)
        {
            return await DeleteEntityAsync(id);
        }
    }
}
