using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Products.Books.BookDictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Products.Books.Dictionaries
{
    /// <summary>
    /// Controller for managing authors.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : BaseController<Author>
    {
        /// <summary>
        /// Initializes a new instance of the AuthorController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public AuthorController(BookStoreContext context) : base(context)
        {
        }

        // GET: api/Author
        /// <summary>
        /// Gets a list of active authors.
        /// </summary>
        /// <returns>List of active authors.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            return await GetAllEntitiesAsync();
        }

        // GET: api/Author/{id}
        /// <summary>
        /// Gets a author by its ID.
        /// </summary>
        /// <param name="id">The ID of the author to retrieve.</param>
        /// <returns>The author with the specified ID or NotFound if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            return await GetEntityByIdAsync(id);
        }

        // POST: api/Author
        /// <summary>
        /// Creates a new author.
        /// </summary>
        /// <param name="author">The new author to create.</param>
        /// <returns>The created author.</returns>
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
            return await CreateEntityAsync(author);
        }

        // PUT: api/Author/{id}
        /// <summary>
        /// Updates an existing author by its ID.
        /// </summary>
        /// <param name="id">The ID of the author to update.</param>
        /// <param name="updatedAuthor">The updated author data.</param>
        /// <returns>The updated author or NotFound if not found.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, [FromBody] Author updatedAuthor)
        {
            return await UpdateEntityAsync(id, updatedAuthor);
        }

        // DELETE: api/Author/{id}
        /// <summary>
        /// Deactivates an existing author by its ID.
        /// </summary>
        /// <param name="id">The ID of the author to deactivate.</param>
        /// <returns>NoContent or NotFound if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            return await DeleteEntityAsync(id);
        }
    }
}
