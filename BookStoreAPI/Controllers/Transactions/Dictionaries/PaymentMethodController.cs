using BookStoreAPI.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreAPI.Models.Transactions.Dictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

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
