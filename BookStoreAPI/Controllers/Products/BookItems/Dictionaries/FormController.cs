using BookStoreData.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Models.Products.BookItems.BookItemDictionaries;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Products.BookItems.Dictionaries
{
    /// <summary>
    /// Controller for managing forms.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : CRUDController<Form>
    {
        /// <summary>
        /// Initializes a new instance of the FormController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public FormController(BookStoreContext context) : base(context)
        {
        }

    }
}
