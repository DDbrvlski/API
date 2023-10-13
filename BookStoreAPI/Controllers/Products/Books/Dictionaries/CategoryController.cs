using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Products.Books.BookDictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Products.Books.Dictionaries
{
    /// <summary>
    /// Controller for managing categories.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : CRUDController<Category>
    {
        /// <summary>
        /// Initializes a new instance of the CategoryController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public CategoryController(BookStoreContext context) : base(context)
        {
        }

    }
}
