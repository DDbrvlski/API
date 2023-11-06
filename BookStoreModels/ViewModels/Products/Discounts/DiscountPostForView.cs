using BookStoreViewModels.ViewModels.Helpers;

namespace BookStoreViewModels.ViewModels.Products.Discounts
{
    public class DiscountPostForView : DiscountPostView
    {
        public string Title { get; set; }
        public List<ListOfIds>? ListOfBookItems { get; set; }
    }
}
