using BookStoreData.Data;
using BookStoreAPI.Helpers;
using BookStoreData.Models.Orders;
using BookStoreData.Models.Transactions;
using BookStoreViewModels.ViewModels.Payments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.BusinessLogic.OrderLogic
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
                    await DatabaseOperationHandler.TryToSaveChangesAsync(_context);
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
            await DatabaseOperationHandler.TryToSaveChangesAsync(_context);

            order.PaymentID = newPayment.Id;
        }

        public static async Task DeactivatePayment(Order order, BookStoreContext _context)
        {
            var paymentToUpdate = await _context.Payment.FirstAsync(x => x.Id == order.PaymentID);

            paymentToUpdate.IsActive = false;

            await DatabaseOperationHandler.TryToSaveChangesAsync(_context);
        }
    }
}
