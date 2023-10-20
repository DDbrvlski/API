using BookStoreAPI.ViewModels.Helpers;

namespace BookStoreAPI.ViewModels.Products.Discounts
{
    public class DiscountPostForView : DiscountView
    {
        public string Title { get; set; }
        public List<ListOfIds>? ListOfBookItems { get; set; }
    }
}
