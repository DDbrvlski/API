using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Accounts.Dictionaries;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Accounts.Dictionaries
{
    /// <summary>
    /// Controller for managing permissions.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : BaseController<Permission>
    {
        /// <summary>
        /// Initializes a new instance of the PermissionController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public PermissionController(BookStoreContext context) : base(context)
        {
        }

        // GET: api/Permission
        /// <summary>
        /// Gets a list of active permissions.
        /// </summary>
        /// <returns>List of active permissions.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Permission>>> GetPermissions()
        {
            return await GetAllEntitiesAsync();
        }

        // GET: api/Permission/{id}
        /// <summary>
        /// Gets a permission by its ID.
        /// </summary>
        /// <param name="id">The ID of the permission to retrieve.</param>
        /// <returns>The permission with the specified ID or NotFound if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Permission>> GetPermission(int id)
        {
            return await GetEntityByIdAsync(id);
        }

        // POST: api/Permission
        /// <summary>
        /// Creates a new permission.
        /// </summary>
        /// <param name="paymentMethod">The new permission to create.</param>
        /// <returns>The created permission.</returns>
        [HttpPost]
        public async Task<ActionResult<Permission>> PostPermission(Permission permission)
        {
            return await CreateEntityAsync(permission);
        }

        // PUT: api/Permission/{id}
        /// <summary>
        /// Updates an existing permission by its ID.
        /// </summary>
        /// <param name="id">The ID of the permission to update.</param>
        /// <param name="updatedPaymentMethod">The updated permission data.</param>
        /// <returns>The updated permission or NotFound if not found.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPermission(int id, [FromBody] Permission updatedPermission)
        {
            return await UpdateEntityAsync(id, updatedPermission);
        }

        // DELETE: api/Permission/{id}
        /// <summary>
        /// Deactivates an existing permission by its ID.
        /// </summary>
        /// <param name="id">The ID of the permission to deactivate.</param>
        /// <returns>NoContent or NotFound if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermission(int id)
        {
            return await DeleteEntityAsync(id);
        }
    }
}
