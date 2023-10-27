using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Customers;
using BookStoreAPI.Models.Delivery;
using BookStoreAPI.Models.Orders;
using BookStoreAPI.Models.Products.Books;
using BookStoreAPI.Models.Transactions;
using BookStoreAPI.ViewModels.Payments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Models.BusinessLogic.OrderLogic
{
    public class OrderPaymentManager
    {
        public static async Task UpdatePayment(Order order, PaymentPostForView payment, BookStoreContext _context)
        {
            if (order.PaymentID == null)
            {
                await AddNewPayment(order, payment, _context);
            }
            else
            {
                var paymentToUpdate = await _context.Payment.FirstAsync(x => x.Id == order.PaymentID);

                if (paymentToUpdate != null)
                {
                    paymentToUpdate.CopyProperties(payment);
                    paymentToUpdate.PaymentMethodID = payment.PaymentMethod.Id;
                    paymentToUpdate.TransactionStatusID = payment.TransactionStatus.Id;
                    _context.SaveChanges();
                }
            }
        }

        public static async Task AddNewPayment(Order order, PaymentPostForView payment, BookStoreContext _context)
        {
            Payment newPayment = new Payment();
            newPayment.CopyProperties(payment);
            newPayment.PaymentMethodID = payment.PaymentMethod.Id;
            newPayment.TransactionStatusID = payment.TransactionStatus.Id;
            _context.Payment.Add(newPayment);
            await TryToSaveChangesAsync(_context);

            order.PaymentID = newPayment.Id;
        }

        public static async Task DeactivatePayment(Order order, BookStoreContext _context)
        {
            var paymentToUpdate = await _context.Payment.FirstAsync(x => x.Id == order.PaymentID);

            paymentToUpdate.IsActive = false;

            _context.SaveChanges();
        }

        private static async Task<IActionResult> TryToSaveChangesAsync(BookStoreContext context)
        {
            try
            {
                await context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception ex)
            {
                Exception innerException = ex.InnerException;

                if (innerException is not null)
                {
                    return new BadRequestObjectResult($"Wewnętrzny wyjątek: {innerException.Message}");
                }
                else
                {
                    return new BadRequestObjectResult($"Wystąpił błąd podczas zapisywania zmian w bazie danych: {ex.Message}");
                }
            }
        }
    }
}
