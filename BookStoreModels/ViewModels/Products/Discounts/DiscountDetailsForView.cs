using BookStoreViewModels.ViewModels.Helpers;
using BookStoreViewModels.ViewModels.Products.BookItems;

namespace BookStoreViewModels.ViewModels.Products.Discounts
{
    public class DiscountDetailsForView : DiscountView
    {
        public string Title { get; set; }
        public bool IsAvailable { get; set; }
        public List<BookItemsForDiscountForView>? ListOfBookItems { get; set; }
    }
}
