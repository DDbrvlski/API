﻿using BookStoreViewModels.ViewModels.Helpers;
using BookStoreViewModels.ViewModels.Payments.Dictionaries;

namespace BookStoreViewModels.ViewModels.Payments
{
    public class PaymentPostForView : BasePostView
    {
        public DateTime? PaymentDate { get; set; }
        public PaymentMethodForView PaymentMethod { get; set; }
        public TransactionStatusForView TransactionStatus { get; set; }
    }
}
