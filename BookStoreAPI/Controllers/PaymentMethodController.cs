using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Transactions.Dictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : BaseController
    {
        public PaymentMethodController(BookStoreContext context) : base(context)
        {
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

        // GET: api/PaymentMethods/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentMethod>> GetPaymentMethod(int id)
        {
            if (_context.PaymentMethod == null)
            {
                return NotFound();
            }
            var paymentMethod = await EntityExistsAsync<PaymentMethod>(id);

            if (paymentMethod == null)
            {
                return NotFound();
            }
            
            return paymentMethod;
        }

        // POST: api/PaymentMethods
        [HttpPost]
        public async Task<ActionResult<PaymentMethod>> PostPaymentMethod(PaymentMethod paymentMethod)
        {
            if (_context.PaymentMethod == null)
            {
                return NotFound();
            }
            _context.PaymentMethod.Add(paymentMethod);
            await _context.SaveChangesAsync();

            return Ok(paymentMethod);
        }

        // PUT: api/PaymentMethod/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentMethod(int id, [FromBody] PaymentMethod updatedPaymentMethod)
        {
            if (updatedPaymentMethod == null)
            {
                return BadRequest("Nieprawidłowe dane.");
            }

            var paymentMethod = await EntityExistsAsync<PaymentMethod>(id);

            if (paymentMethod == null)
            {
                return NotFound("Metoda płatności o podanym ID nie istnieje.");
            }

            paymentMethod.CopyProperties(updatedPaymentMethod);
            _context.Entry(paymentMethod).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (EntityExistsAsync<PaymentMethod>(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(paymentMethod);
        }

        // DELETE: api/PaymentMethod/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentMethod(int id)
        {
            if (_context.PaymentMethod == null)
            {
                return NotFound();
            }
            var paymentMethod = await EntityExistsAsync<PaymentMethod>(id);

            if (paymentMethod == null)
            {
                return NotFound();
            }

            paymentMethod.IsActive = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
