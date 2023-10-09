using BookStoreAPI.Data;
using BookStoreAPI.Models.Transactions.Dictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly BookStoreContext _context;

        public PaymentMethodController(BookStoreContext context)
        {
            _context = context;
        }

        // GET: api/PaymentMethods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentMethod>>> GetPaymentMethods()
        {
            if (_context.PaymentMethod == null)
            {
                return NotFound();
            }
            var result = _context.PaymentMethod.Where(x => x.IsActive == true).ToList();
            return result;
        }

        // POST: api/PaymentMethods
        [HttpPost]
        public async Task<ActionResult<PaymentMethod>> PostPaymentMethod(PaymentMethod paymentMethod)
        {
            if (_context.PaymentMethod == null)
            {
                return Problem("Entity set 'BookStoreContext.PaymentMethod'  is null.");
            }
            _context.PaymentMethod.Add(paymentMethod);
            await _context.SaveChangesAsync();

            return Ok(paymentMethod);
        }
    }
}
