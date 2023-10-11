using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Products.BookItems.BookItemDictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Products.BookItems.Dictionaries
{
    /// <summary>
    /// Controller for managing forms.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : BaseController<Form>
    {
        /// <summary>
        /// Initializes a new instance of the FormController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public FormController(BookStoreContext context) : base(context)
        {
        }

        // GET: api/Form
        /// <summary>
        /// Gets a list of active forms.
        /// </summary>
        /// <returns>List of active forms.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Form>>> GetForms()
        {
            return await GetAllEntitiesAsync();
        }

        // GET: api/Form/{id}
        /// <summary>
        /// Gets a form by its ID.
        /// </summary>
        /// <param name="id">The ID of the form to retrieve.</param>
        /// <returns>The form with the specified ID or NotFound if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Form>> GetForm(int id)
        {
            return await GetEntityByIdAsync(id);
        }

        // POST: api/Form
        /// <summary>
        /// Creates a new form.
        /// </summary>
        /// <param name="form">The new form to create.</param>
        /// <returns>The created form.</returns>
        [HttpPost]
        public async Task<ActionResult<Form>> PostForm(Form form)
        {
            return await CreateEntityAsync(form);
        }

        // PUT: api/Form/{id}
        /// <summary>
        /// Updates an existing form by its ID.
        /// </summary>
        /// <param name="id">The ID of the form to update.</param>
        /// <param name="updatedForm">The updated form data.</param>
        /// <returns>The updated form or NotFound if not found.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutForm(int id, [FromBody] Form updatedForm)
        {
            return await UpdateEntityAsync(id, updatedForm);
        }

        // DELETE: api/Form/{id}
        /// <summary>
        /// Deactivates an existing form by its ID.
        /// </summary>
        /// <param name="id">The ID of the form to deactivate.</param>
        /// <returns>NoContent or NotFound if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteForm(int id)
        {
            return await DeleteEntityAsync(id);
        }
    }
}
