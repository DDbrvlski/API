using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Orders.Dictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Orders.Dictionaries
{
    /// <summary>
    /// Controller for managing order statuses.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusController : BaseController<OrderStatus>
    {
        /// <summary>
        /// Initializes a new instance of the OrderStatusController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public OrderStatusController(BookStoreContext context) : base(context)
        {
        }

        // GET: api/OrderStatus
        /// <summary>
        /// Gets a list of active orderStatuss.
        /// </summary>
        /// <returns>List of active orderStatuss.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderStatus>>> GetOrderStatuss()
        {
            return await GetAllEntitiesAsync();
        }

        // GET: api/OrderStatus/{id}
        /// <summary>
        /// Gets a orderStatus by its ID.
        /// </summary>
        /// <param name="id">The ID of the orderStatus to retrieve.</param>
        /// <returns>The orderStatus with the specified ID or NotFound if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderStatus>> GetOrderStatus(int id)
        {
            return await GetEntityByIdAsync(id);
        }

        // POST: api/OrderStatus
        /// <summary>
        /// Creates a new orderStatus.
        /// </summary>
        /// <param name="orderStatus">The new orderStatus to create.</param>
        /// <returns>The created orderStatus.</returns>
        [HttpPost]
        public async Task<ActionResult<OrderStatus>> PostOrderStatus(OrderStatus orderStatus)
        {
            return await CreateEntityAsync(orderStatus);
        }

        // PUT: api/OrderStatus/{id}
        /// <summary>
        /// Updates an existing orderStatus by its ID.
        /// </summary>
        /// <param name="id">The ID of the orderStatus to update.</param>
        /// <param name="updatedOrderStatus">The updated orderStatus data.</param>
        /// <returns>The updated orderStatus or NotFound if not found.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderStatus(int id, [FromBody] OrderStatus updatedOrderStatus)
        {
            return await UpdateEntityAsync(id, updatedOrderStatus);
        }

        // DELETE: api/OrderStatus/{id}
        /// <summary>
        /// Deactivates an existing orderStatus by its ID.
        /// </summary>
        /// <param name="id">The ID of the orderStatus to deactivate.</param>
        /// <returns>NoContent or NotFound if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderStatus(int id)
        {
            return await DeleteEntityAsync(id);
        }
    }
}
