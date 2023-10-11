using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Rentals.Dictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Rentals.Dictionaries
{
    /// <summary>
    /// Controller for managing rental statuses.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RentalStatusController : BaseController<RentalStatus>
    {
        /// <summary>
        /// Initializes a new instance of the RentalStatusController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public RentalStatusController(BookStoreContext context) : base(context)
        {
        }

        // GET: api/RentalStatus
        /// <summary>
        /// Gets a list of active genders.
        /// </summary>
        /// <returns>List of active genders.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentalStatus>>> GetRentalStatuss()
        {
            return await GetAllEntitiesAsync();
        }

        // GET: api/RentalStatus/{id}
        /// <summary>
        /// Gets a gender by its ID.
        /// </summary>
        /// <param name="id">The ID of the gender to retrieve.</param>
        /// <returns>The gender with the specified ID or NotFound if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<RentalStatus>> GetRentalStatus(int id)
        {
            return await GetEntityByIdAsync(id);
        }

        // POST: api/RentalStatus
        /// <summary>
        /// Creates a new gender.
        /// </summary>
        /// <param name="gender">The new gender to create.</param>
        /// <returns>The created gender.</returns>
        [HttpPost]
        public async Task<ActionResult<RentalStatus>> PostRentalStatus(RentalStatus gender)
        {
            return await CreateEntityAsync(gender);
        }

        // PUT: api/RentalStatus/{id}
        /// <summary>
        /// Updates an existing gender by its ID.
        /// </summary>
        /// <param name="id">The ID of the gender to update.</param>
        /// <param name="updatedRentalStatus">The updated gender data.</param>
        /// <returns>The updated gender or NotFound if not found.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRentalStatus(int id, [FromBody] RentalStatus updatedRentalStatus)
        {
            return await UpdateEntityAsync(id, updatedRentalStatus);
        }

        // DELETE: api/RentalStatus/{id}
        /// <summary>
        /// Deactivates an existing gender by its ID.
        /// </summary>
        /// <param name="id">The ID of the gender to deactivate.</param>
        /// <returns>NoContent or NotFound if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRentalStatus(int id)
        {
            return await DeleteEntityAsync(id);
        }
    }
}
