using BookStoreAPI.ViewModels.Helpers;

namespace BookStoreAPI.ViewModels.Products.DiscountCodes
{
    public class DiscountCodePostForView : DiscountView
    {
        public string Code { get; set; }
        public List<ListOfIds>? ListOfBookItems { get; set; }
    }
}
