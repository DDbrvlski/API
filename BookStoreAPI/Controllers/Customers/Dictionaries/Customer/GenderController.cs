using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Transactions.Dictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Customers.Dictionaries.Customer
{
    /// <summary>
    /// Controller for managing genders.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : BaseController<Gender>
    {
        /// <summary>
        /// Initializes a new instance of the GenderController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public GenderController(BookStoreContext context) : base(context)
        {
        }

        // GET: api/Gender
        /// <summary>
        /// Gets a list of active genders.
        /// </summary>
        /// <returns>List of active genders.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gender>>> GetGenders()
        {
            return await GetAllEntitiesAsync();
        }

        // GET: api/Gender/{id}
        /// <summary>
        /// Gets a gender by its ID.
        /// </summary>
        /// <param name="id">The ID of the gender to retrieve.</param>
        /// <returns>The gender with the specified ID or NotFound if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Gender>> GetGender(int id)
        {
            return await GetEntityByIdAsync(id);
        }

        // POST: api/Gender
        /// <summary>
        /// Creates a new gender.
        /// </summary>
        /// <param name="gender">The new gender to create.</param>
        /// <returns>The created gender.</returns>
        [HttpPost]
        public async Task<ActionResult<Gender>> PostGender(Gender gender)
        {
            return await CreateEntityAsync(gender);
        }

        // PUT: api/Gender/{id}
        /// <summary>
        /// Updates an existing gender by its ID.
        /// </summary>
        /// <param name="id">The ID of the gender to update.</param>
        /// <param name="updatedGender">The updated gender data.</param>
        /// <returns>The updated gender or NotFound if not found.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGender(int id, [FromBody] Gender updatedGender)
        {
            return await UpdateEntityAsync(id, updatedGender);
        }

        // DELETE: api/Gender/{id}
        /// <summary>
        /// Deactivates an existing gender by its ID.
        /// </summary>
        /// <param name="id">The ID of the gender to deactivate.</param>
        /// <returns>NoContent or NotFound if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGender(int id)
        {
            return await DeleteEntityAsync(id);
        }
    }
}
