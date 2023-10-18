using BookStoreAPI.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Products.Books.BookDictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Products.Books.Dictionaries
{
    /// <summary>
    /// Controller for managing languages.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : CRUDController<Language>
    {
        /// <summary>
        /// Initializes a new instance of the LanguageController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public LanguageController(BookStoreContext context) : base(context)
        {
        }

    }
}
