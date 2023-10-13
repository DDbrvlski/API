using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Products.BookItems.BookItemDictionaries;
using Microsoft.AspNetCore.Http;
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
