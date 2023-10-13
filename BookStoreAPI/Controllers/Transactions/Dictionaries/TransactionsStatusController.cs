using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Transactions.Dictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Transactions.Dictionaries
{
    /// <summary>
    /// Controller for managing transaction status.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsStatusController : CRUDController<TransactionsStatus>
    {
        /// <summary>
        /// Initializes a new instance of the TransactionsStatusController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public TransactionsStatusController(BookStoreContext context) : base(context)
        {
        }

    }
}
