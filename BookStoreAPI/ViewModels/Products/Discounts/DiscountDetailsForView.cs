using BookStoreAPI.ViewModels.Helpers;
using BookStoreAPI.ViewModels.Products.BookItems;

namespace BookStoreAPI.ViewModels.Products.Discounts
{
    public class DiscountDetailsForView : DiscountView
    {
        public string Title { get; set; }
        public bool IsAvailable { get; set; }
        public List<BookItemsForDiscountForView>? ListOfBookItems { get; set; }
    }
}
