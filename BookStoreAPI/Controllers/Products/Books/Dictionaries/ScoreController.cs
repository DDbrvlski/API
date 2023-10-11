using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Products.Books.BookDictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Products.Books.Dictionaries
{
    /// <summary>
    /// Controller for managing scores.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : BaseController<Score>
    {
        /// <summary>
        /// Initializes a new instance of the ScoreController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ScoreController(BookStoreContext context) : base(context)
        {
        }

        // GET: api/Score
        /// <summary>
        /// Gets a list of active scores.
        /// </summary>
        /// <returns>List of active scores.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Score>>> GetScores()
        {
            return await GetAllEntitiesAsync();
        }

        // GET: api/Score/{id}
        /// <summary>
        /// Gets a score by its ID.
        /// </summary>
        /// <param name="id">The ID of the score to retrieve.</param>
        /// <returns>The score with the specified ID or NotFound if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Score>> GetScore(int id)
        {
            return await GetEntityByIdAsync(id);
        }

        // POST: api/Score
        /// <summary>
        /// Creates a new score.
        /// </summary>
        /// <param name="score">The new score to create.</param>
        /// <returns>The created score.</returns>
        [HttpPost]
        public async Task<ActionResult<Score>> PostScore(Score score)
        {
            return await CreateEntityAsync(score);
        }

        // PUT: api/Score/{id}
        /// <summary>
        /// Updates an existing score by its ID.
        /// </summary>
        /// <param name="id">The ID of the score to update.</param>
        /// <param name="updatedScore">The updated score data.</param>
        /// <returns>The updated score or NotFound if not found.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScore(int id, [FromBody] Score updatedScore)
        {
            return await UpdateEntityAsync(id, updatedScore);
        }

        // DELETE: api/Score/{id}
        /// <summary>
        /// Deactivates an existing score by its ID.
        /// </summary>
        /// <param name="id">The ID of the score to deactivate.</param>
        /// <returns>NoContent or NotFound if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScore(int id)
        {
            return await DeleteEntityAsync(id);
        }
    }
}
