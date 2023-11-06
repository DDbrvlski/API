﻿using BookStoreViewModels.ViewModels.Helpers;
using BookStoreViewModels.ViewModels.Payments.Dictionaries;

namespace BookStoreViewModels.ViewModels.Payments
{
    public class PaymentDetailsForView : BaseView
    {
        public decimal Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public PaymentMethodForView PaymentMethod { get; set; }
        public TransactionStatusForView TransactionStatus { get; set; }
    }
}
