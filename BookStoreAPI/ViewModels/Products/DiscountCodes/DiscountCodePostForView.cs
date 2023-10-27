using BookStoreAPI.ViewModels.Helpers;

namespace BookStoreAPI.ViewModels.Products.DiscountCodes
{
    public class DiscountCodePostForView : DiscountPostView
    {
        public string Code { get; set; }
        public List<ListOfIds>? ListOfBookItems { get; set; }
    }
}
