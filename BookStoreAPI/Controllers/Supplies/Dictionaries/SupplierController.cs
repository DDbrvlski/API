using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Supplies.Dictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Supplies.Dictionaries
{
    /// <summary>
    /// Controller for managing suppliers.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : BaseController<Supplier>
    {
        /// <summary>
        /// Initializes a new instance of the SupplierController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public SupplierController(BookStoreContext context) : base(context)
        {
        }

        // GET: api/Supplier
        /// <summary>
        /// Gets a list of active suppliers.
        /// </summary>
        /// <returns>List of active suppliers.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetSuppliers()
        {
            return await GetAllEntitiesAsync();
        }

        // GET: api/Supplier/{id}
        /// <summary>
        /// Gets a supplier by its ID.
        /// </summary>
        /// <param name="id">The ID of the supplier to retrieve.</param>
        /// <returns>The supplier with the specified ID or NotFound if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetSupplier(int id)
        {
            return await GetEntityByIdAsync(id);
        }

        // POST: api/Supplier
        /// <summary>
        /// Creates a new supplier.
        /// </summary>
        /// <param name="supplier">The new supplier to create.</param>
        /// <returns>The created supplier.</returns>
        [HttpPost]
        public async Task<ActionResult<Supplier>> PostSupplier(Supplier supplier)
        {
            return await CreateEntityAsync(supplier);
        }

        // PUT: api/Supplier/{id}
        /// <summary>
        /// Updates an existing supplier by its ID.
        /// </summary>
        /// <param name="id">The ID of the supplier to update.</param>
        /// <param name="updatedSupplier">The updated supplier data.</param>
        /// <returns>The updated supplier or NotFound if not found.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupplier(int id, [FromBody] Supplier updatedSupplier)
        {
            return await UpdateEntityAsync(id, updatedSupplier);
        }

        // DELETE: api/Supplier/{id}
        /// <summary>
        /// Deactivates an existing supplier by its ID.
        /// </summary>
        /// <param name="id">The ID of the supplier to deactivate.</param>
        /// <returns>NoContent or NotFound if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            return await DeleteEntityAsync(id);
        }
    }
}
