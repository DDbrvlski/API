using BookStoreAPI.ViewModels.Helpers;
using BookStoreAPI.ViewModels.Payments.Dictionaries;

namespace BookStoreAPI.ViewModels.Payments
{
    public class PaymentDetailsForView : BaseView
    {
        public decimal Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public PaymentMethodForView PaymentMethod { get; set; }
        public TransactionStatusForView TransactionStatus { get; set; }
    }
}
