using BookStoreAPI.ViewModels.Helpers;

namespace BookStoreAPI.ViewModels.Products.DiscountCodes
{
    public class DiscountCodeDetailsForView : DiscountView
    {
        public string Code { get; set; }
        public bool IsAvailable { get; set; }
    }
}
