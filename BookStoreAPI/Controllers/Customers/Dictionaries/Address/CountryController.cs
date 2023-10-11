using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Customers.AddressDictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Customers.Dictionaries.Address
{
    /// <summary>
    /// Controller for managing countries.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : BaseController<Country>
    {
        /// <summary>
        /// Initializes a new instance of the CountryController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public CountryController(BookStoreContext context) : base(context)
        {
        }

        // GET: api/Country
        /// <summary>
        /// Gets a list of active countries.
        /// </summary>
        /// <returns>List of active countries.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            return await GetAllEntitiesAsync();
        }

        // GET: api/Country/{id}
        /// <summary>
        /// Gets a country by its ID.
        /// </summary>
        /// <param name="id">The ID of the country to retrieve.</param>
        /// <returns>Country with the specified ID or NotFound if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            return await GetEntityByIdAsync(id);
        }

        // POST: api/Country
        /// <summary>
        /// Creates a new country.
        /// </summary>
        /// <param name="country">The new country to create.</param>
        /// <returns>The created country.</returns>
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(Country country)
        {
            return await CreateEntityAsync(country);
        }

        // PUT: api/Country/{id}
        /// <summary>
        /// Updates an existing country by its ID.
        /// </summary>
        /// <param name="id">The ID of the country to update.</param>
        /// <param name="updatedCountry">The updated country data.</param>
        /// <returns>The updated country or NotFound if not found.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, [FromBody] Country updatedCountry)
        {
            return await UpdateEntityAsync(id, updatedCountry);
        }

        // DELETE: api/Country/{id}
        /// <summary>
        /// Deactivates an existing country by its ID.
        /// </summary>
        /// <param name="id">The ID of the country to deactivate.</param>
        /// <returns>NoContent or NotFound if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            return await DeleteEntityAsync(id);
        }
    }
}
