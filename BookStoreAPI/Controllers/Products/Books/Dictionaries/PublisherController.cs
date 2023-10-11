using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Products.Books.BookDictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Products.Books.Dictionaries
{
    /// <summary>
    /// Controller for managing publishers.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : BaseController<Publisher>
    {
        /// <summary>
        /// Initializes a new instance of the PublisherController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public PublisherController(BookStoreContext context) : base(context)
        {
        }

        // GET: api/Publisher
        /// <summary>
        /// Gets a list of active publishers.
        /// </summary>
        /// <returns>List of active publishers.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetPublishers()
        {
            return await GetAllEntitiesAsync();
        }

        // GET: api/Publisher/{id}
        /// <summary>
        /// Gets a publisher by its ID.
        /// </summary>
        /// <param name="id">The ID of the publisher to retrieve.</param>
        /// <returns>The publisher with the specified ID or NotFound if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Publisher>> GetPublisher(int id)
        {
            return await GetEntityByIdAsync(id);
        }

        // POST: api/Publisher
        /// <summary>
        /// Creates a new publisher.
        /// </summary>
        /// <param name="publisher">The new publisher to create.</param>
        /// <returns>The created publisher.</returns>
        [HttpPost]
        public async Task<ActionResult<Publisher>> PostPublisher(Publisher publisher)
        {
            return await CreateEntityAsync(publisher);
        }

        // PUT: api/Publisher/{id}
        /// <summary>
        /// Updates an existing publisher by its ID.
        /// </summary>
        /// <param name="id">The ID of the publisher to update.</param>
        /// <param name="updatedPublisher">The updated publisher data.</param>
        /// <returns>The updated publisher or NotFound if not found.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublisher(int id, [FromBody] Publisher updatedPublisher)
        {
            return await UpdateEntityAsync(id, updatedPublisher);
        }

        // DELETE: api/Publisher/{id}
        /// <summary>
        /// Deactivates an existing publisher by its ID.
        /// </summary>
        /// <param name="id">The ID of the publisher to deactivate.</param>
        /// <returns>NoContent or NotFound if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            return await DeleteEntityAsync(id);
        }
    }
}
