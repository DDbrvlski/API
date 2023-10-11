using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Products.Books.BookDictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Products.Books.Dictionaries
{
    /// <summary>
    /// Controller for managing languages.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : BaseController<Language>
    {
        /// <summary>
        /// Initializes a new instance of the LanguageController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public LanguageController(BookStoreContext context) : base(context)
        {
        }

        // GET: api/Language
        /// <summary>
        /// Gets a list of active languages.
        /// </summary>
        /// <returns>List of active languages.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Language>>> GetLanguages()
        {
            return await GetAllEntitiesAsync();
        }

        // GET: api/Language/{id}
        /// <summary>
        /// Gets a language by its ID.
        /// </summary>
        /// <param name="id">The ID of the language to retrieve.</param>
        /// <returns>The language with the specified ID or NotFound if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Language>> GetLanguage(int id)
        {
            return await GetEntityByIdAsync(id);
        }

        // POST: api/Language
        /// <summary>
        /// Creates a new language.
        /// </summary>
        /// <param name="language">The new language to create.</param>
        /// <returns>The created language.</returns>
        [HttpPost]
        public async Task<ActionResult<Language>> PostLanguage(Language language)
        {
            return await CreateEntityAsync(language);
        }

        // PUT: api/Language/{id}
        /// <summary>
        /// Updates an existing language by its ID.
        /// </summary>
        /// <param name="id">The ID of the language to update.</param>
        /// <param name="updatedLanguage">The updated language data.</param>
        /// <returns>The updated language or NotFound if not found.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLanguage(int id, [FromBody] Language updatedLanguage)
        {
            return await UpdateEntityAsync(id, updatedLanguage);
        }

        // DELETE: api/Language/{id}
        /// <summary>
        /// Deactivates an existing language by its ID.
        /// </summary>
        /// <param name="id">The ID of the language to deactivate.</param>
        /// <returns>NoContent or NotFound if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLanguage(int id)
        {
            return await DeleteEntityAsync(id);
        }
    }
}
