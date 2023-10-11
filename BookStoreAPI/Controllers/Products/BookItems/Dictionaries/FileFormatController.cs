using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Products.BookItems.BookItemDictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Products.BookItems.Dictionaries
{
    /// <summary>
    /// Controller for managing file formats.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FileFormatController : BaseController<FileFormat>
    {
        /// <summary>
        /// Initializes a new instance of the FileFormatController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public FileFormatController(BookStoreContext context) : base(context)
        {
        }

        // GET: api/FileFormat
        /// <summary>
        /// Gets a list of active fileFormats.
        /// </summary>
        /// <returns>List of active fileFormats.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FileFormat>>> GetFileFormats()
        {
            return await GetAllEntitiesAsync();
        }

        // GET: api/FileFormat/{id}
        /// <summary>
        /// Gets a fileFormat by its ID.
        /// </summary>
        /// <param name="id">The ID of the fileFormat to retrieve.</param>
        /// <returns>The fileFormat with the specified ID or NotFound if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<FileFormat>> GetFileFormat(int id)
        {
            return await GetEntityByIdAsync(id);
        }

        // POST: api/FileFormat
        /// <summary>
        /// Creates a new fileFormat.
        /// </summary>
        /// <param name="fileFormat">The new fileFormat to create.</param>
        /// <returns>The created fileFormat.</returns>
        [HttpPost]
        public async Task<ActionResult<FileFormat>> PostFileFormat(FileFormat fileFormat)
        {
            return await CreateEntityAsync(fileFormat);
        }

        // PUT: api/FileFormat/{id}
        /// <summary>
        /// Updates an existing fileFormat by its ID.
        /// </summary>
        /// <param name="id">The ID of the fileFormat to update.</param>
        /// <param name="updatedFileFormat">The updated fileFormat data.</param>
        /// <returns>The updated fileFormat or NotFound if not found.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFileFormat(int id, [FromBody] FileFormat updatedFileFormat)
        {
            return await UpdateEntityAsync(id, updatedFileFormat);
        }

        // DELETE: api/FileFormat/{id}
        /// <summary>
        /// Deactivates an existing fileFormat by its ID.
        /// </summary>
        /// <param name="id">The ID of the fileFormat to deactivate.</param>
        /// <returns>NoContent or NotFound if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFileFormat(int id)
        {
            return await DeleteEntityAsync(id);
        }
    }
}
