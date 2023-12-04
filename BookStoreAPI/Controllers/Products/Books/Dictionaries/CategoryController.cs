using BookStoreData.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Models.Products.Books.BookDictionaries;
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
