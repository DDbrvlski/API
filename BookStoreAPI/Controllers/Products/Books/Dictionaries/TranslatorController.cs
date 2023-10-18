using BookStoreAPI.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Products.Books.BookDictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Products.Books.Dictionaries
{
    /// <summary>
    /// Controller for managing translators.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TranslatorController : CRUDController<Translator>
    {
        /// <summary>
        /// Initializes a new instance of the TranslatorController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public TranslatorController(BookStoreContext context) : base(context)
        {
        }

    }
}
