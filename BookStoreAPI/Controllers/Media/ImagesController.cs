using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Media;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Media
{
    /// <summary>
    /// Controller for managing images.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : CRUDController<Images>
    {
        /// <summary>
        /// Initializes a new instance of the ImagesController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ImagesController(BookStoreContext context) : base(context)
        {
        }
    }
}
