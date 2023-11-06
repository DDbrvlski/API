using BookStoreData.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Models.Supplies.Dictionaries;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Supplies.Dictionaries
{
    /// <summary>
    /// Controller for managing suppliers.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : CRUDController<Supplier>
    {
        /// <summary>
        /// Initializes a new instance of the SupplierController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public SupplierController(BookStoreContext context) : base(context)
        {
        }

    }
}
