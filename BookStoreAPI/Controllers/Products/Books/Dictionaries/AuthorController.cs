using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Products.Books.BookDictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Products.Books.Dictionaries
{
    /// <summary>
    /// Controller for managing authors.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : CRUDController<Author>
    {
        /// <summary>
        /// Initializes a new instance of the AuthorController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public AuthorController(BookStoreContext context) : base(context)
        {
        }

    }
}
