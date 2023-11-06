using BookStoreData.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Models.Transactions.Dictionaries;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Transactions.Dictionaries
{
    /// <summary>
    /// Controller for managing payment methods.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : CRUDController<PaymentMethod>
    {
        /// <summary>
        /// Initializes a new instance of the PaymentMethodController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public PaymentMethodController(BookStoreContext context) : base(context)
        {
        }

    }
}
