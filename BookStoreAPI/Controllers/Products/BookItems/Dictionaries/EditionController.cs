using BookStoreAPI.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Products.BookItems.BookItemDictionaries;
using Microsoft.AspNetCore.Http;
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
