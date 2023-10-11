using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Customers.AddressDictionaries;
using BookStoreAPI.Models.Transactions.Dictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Customers.Dictionaries.Address
{
    /// <summary>
    /// Controller for managing cities.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : BaseController<City>
    {
        /// <summary>
        /// Initializes a new instance of the CityController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public CityController(BookStoreContext context) : base(context)
        {
        }

        // GET: api/City
        /// <summary>
        /// Gets a list of active cities.
        /// </summary>
        /// <returns>List of active cities.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            return await GetAllEntitiesAsync();
        }

        // GET: api/City/{id}
        /// <summary>
        /// Gets a city by its ID.
        /// </summary>
        /// <param name="id">The ID of the city to retrieve.</param>
        /// <returns>City with the specified ID or NotFound if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(int id)
        {
            return await GetEntityByIdAsync(id);
        }

        // POST: api/City
        /// <summary>
        /// Creates a new city.
        /// </summary>
        /// <param name="city">The new city to create.</param>
        /// <returns>The created city.</returns>
        [HttpPost]
        public async Task<ActionResult<City>> PostCity(City city)
        {
            return await CreateEntityAsync(city);
        }

        // PUT: api/City/{id}
        /// <summary>
        /// Updates an existing city by its ID.
        /// </summary>
        /// <param name="id">The ID of the city to update.</param>
        /// <param name="updatedCity">The updated city data.</param>
        /// <returns>The updated city or NotFound if not found.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(int id, [FromBody] City updatedCity)
        {
            return await UpdateEntityAsync(id, updatedCity);
        }

        // DELETE: api/City/{id}
        /// <summary>
        /// Deactivates an existing city by its ID.
        /// </summary>
        /// <param name="id">The ID of the city to deactivate.</param>
        /// <returns>NoContent or NotFound if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            return await DeleteEntityAsync(id);
        }
    }
}
