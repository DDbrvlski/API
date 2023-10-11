using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Products.Books.BookDictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Products.Books.Dictionaries
{
    /// <summary>
    /// Controller for managing translators.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TranslatorController : BaseController<Translator>
    {
        /// <summary>
        /// Initializes a new instance of the TranslatorController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public TranslatorController(BookStoreContext context) : base(context)
        {
        }

        // GET: api/Translator
        /// <summary>
        /// Gets a list of active translators.
        /// </summary>
        /// <returns>List of active translators.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Translator>>> GetTranslators()
        {
            return await GetAllEntitiesAsync();
        }

        // GET: api/Translator/{id}
        /// <summary>
        /// Gets a translator by its ID.
        /// </summary>
        /// <param name="id">The ID of the translator to retrieve.</param>
        /// <returns>The translator with the specified ID or NotFound if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Translator>> GetTranslator(int id)
        {
            return await GetEntityByIdAsync(id);
        }

        // POST: api/Translator
        /// <summary>
        /// Creates a new translator.
        /// </summary>
        /// <param name="translator">The new translator to create.</param>
        /// <returns>The created translator.</returns>
        [HttpPost]
        public async Task<ActionResult<Translator>> PostTranslator(Translator translator)
        {
            return await CreateEntityAsync(translator);
        }

        // PUT: api/Translator/{id}
        /// <summary>
        /// Updates an existing translator by its ID.
        /// </summary>
        /// <param name="id">The ID of the translator to update.</param>
        /// <param name="updatedTranslator">The updated translator data.</param>
        /// <returns>The updated translator or NotFound if not found.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTranslator(int id, [FromBody] Translator updatedTranslator)
        {
            return await UpdateEntityAsync(id, updatedTranslator);
        }

        // DELETE: api/Translator/{id}
        /// <summary>
        /// Deactivates an existing translator by its ID.
        /// </summary>
        /// <param name="id">The ID of the translator to deactivate.</param>
        /// <returns>NoContent or NotFound if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTranslator(int id)
        {
            return await DeleteEntityAsync(id);
        }
    }
}
