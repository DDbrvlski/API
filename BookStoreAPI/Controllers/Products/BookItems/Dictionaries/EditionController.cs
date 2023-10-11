using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Products.BookItems.BookItemDictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Products.BookItems.Dictionaries
{
    /// <summary>
    /// Controller for managing editions.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EditionController : BaseController<Edition>
    {
        /// <summary>
        /// Initializes a new instance of the EditionController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public EditionController(BookStoreContext context) : base(context)
        {
        }

        // GET: api/Edition
        /// <summary>
        /// Gets a list of active editions.
        /// </summary>
        /// <returns>List of active editions.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Edition>>> GetEditions()
        {
            return await GetAllEntitiesAsync();
        }

        // GET: api/Edition/{id}
        /// <summary>
        /// Gets a edition by its ID.
        /// </summary>
        /// <param name="id">The ID of the edition to retrieve.</param>
        /// <returns>The edition with the specified ID or NotFound if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Edition>> GetEdition(int id)
        {
            return await GetEntityByIdAsync(id);
        }

        // POST: api/Edition
        /// <summary>
        /// Creates a new edition.
        /// </summary>
        /// <param name="edition">The new edition to create.</param>
        /// <returns>The created edition.</returns>
        [HttpPost]
        public async Task<ActionResult<Edition>> PostEdition(Edition edition)
        {
            return await CreateEntityAsync(edition);
        }

        // PUT: api/Edition/{id}
        /// <summary>
        /// Updates an existing edition by its ID.
        /// </summary>
        /// <param name="id">The ID of the edition to update.</param>
        /// <param name="updatedEdition">The updated edition data.</param>
        /// <returns>The updated edition or NotFound if not found.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEdition(int id, [FromBody] Edition updatedEdition)
        {
            return await UpdateEntityAsync(id, updatedEdition);
        }

        // DELETE: api/Edition/{id}
        /// <summary>
        /// Deactivates an existing edition by its ID.
        /// </summary>
        /// <param name="id">The ID of the edition to deactivate.</param>
        /// <returns>NoContent or NotFound if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEdition(int id)
        {
            return await DeleteEntityAsync(id);
        }
    }
}
