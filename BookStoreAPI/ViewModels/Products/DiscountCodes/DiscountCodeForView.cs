using BookStoreAPI.ViewModels.Helpers;

namespace BookStoreAPI.ViewModels.Products.DiscountCodes
{
    public class DiscountCodeForView : BaseView
    {
        public string Code { get; set; }
        public decimal PercentOfDiscount { get; set; }
        public bool IsAvailable { get; set; }
        public string Description { get; set; }
    }
}
