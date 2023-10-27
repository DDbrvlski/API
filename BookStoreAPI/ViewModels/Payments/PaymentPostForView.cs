using BookStoreAPI.ViewModels.Helpers;
using BookStoreAPI.ViewModels.Payments.Dictionaries;

namespace BookStoreAPI.ViewModels.Payments
{
    public class PaymentPostForView : BasePostView
    {
        public DateTime? PaymentDate { get; set; }
        public PaymentMethodForView PaymentMethod { get; set; }
        public TransactionStatusForView TransactionStatus { get; set; }
    }
}
