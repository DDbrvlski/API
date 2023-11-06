using BookStoreData.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Models.Products.Books.BookDictionaries;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Products.Books.Dictionaries
{
    /// <summary>
    /// Controller for managing scores.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : CRUDController<Score>
    {
        /// <summary>
        /// Initializes a new instance of the ScoreController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ScoreController(BookStoreContext context) : base(context)
        {
        }

    }
}
