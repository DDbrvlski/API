﻿using BookStoreViewModels.ViewModels.Helpers;

namespace BookStoreViewModels.ViewModels.Products.Discounts
{
    public class DiscountForView : BaseView
    {
        public string Title { get; set; }
        public decimal PercentOfDiscount { get; set; }
        public bool IsAvailable { get; set; }
        public string Description { get; set; }
    }
}
