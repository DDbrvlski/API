using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Products.BookItems.BookItemDictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Products.BookItems.Dictionaries
{
    /// <summary>
    /// Controller for managing file formats.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FileFormatController : CRUDController<FileFormat>
    {
        /// <summary>
        /// Initializes a new instance of the FileFormatController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public FileFormatController(BookStoreContext context) : base(context)
        {
        }

    }
}
