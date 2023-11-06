using BookStoreData.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Models.Products.Books.BookDictionaries;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Products.Books.Dictionaries
{
    /// <summary>
    /// Controller for managing publishers.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : CRUDController<Publisher>
    {
        /// <summary>
        /// Initializes a new instance of the PublisherController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public PublisherController(BookStoreContext context) : base(context)
        {
        }

    }
}
