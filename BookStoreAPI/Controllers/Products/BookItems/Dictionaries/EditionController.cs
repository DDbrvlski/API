using BookStoreData.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Models.Products.BookItems.BookItemDictionaries;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Products.BookItems.Dictionaries
{
    /// <summary>
    /// Controller for managing editions.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EditionController : CRUDController<Edition>
    {
        /// <summary>
        /// Initializes a new instance of the EditionController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public EditionController(BookStoreContext context) : base(context)
        {
        }

    }
}
