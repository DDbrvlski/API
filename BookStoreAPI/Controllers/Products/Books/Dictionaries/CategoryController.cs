using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Products.Books.BookDictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Products.Books.Dictionaries
{
    /// <summary>
    /// Controller for managing categories.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController<Category>
    {
        /// <summary>
        /// Initializes a new instance of the CategoryController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public CategoryController(BookStoreContext context) : base(context)
        {
        }

        // GET: api/Category
        /// <summary>
        /// Gets a list of active categories.
        /// </summary>
        /// <returns>List of active categories.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await GetAllEntitiesAsync();
        }

        // GET: api/Category/{id}
        /// <summary>
        /// Gets a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>The category with the specified ID or NotFound if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            return await GetEntityByIdAsync(id);
        }

        // POST: api/Category
        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="category">The new category to create.</param>
        /// <returns>The created category.</returns>
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            return await CreateEntityAsync(category);
        }

        // PUT: api/Category/{id}
        /// <summary>
        /// Updates an existing category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to update.</param>
        /// <param name="updatedCategory">The updated category data.</param>
        /// <returns>The updated category or NotFound if not found.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, [FromBody] Category updatedCategory)
        {
            return await UpdateEntityAsync(id, updatedCategory);
        }

        // DELETE: api/Category/{id}
        /// <summary>
        /// Deactivates an existing category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to deactivate.</param>
        /// <returns>NoContent or NotFound if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            return await DeleteEntityAsync(id);
        }
    }
}
