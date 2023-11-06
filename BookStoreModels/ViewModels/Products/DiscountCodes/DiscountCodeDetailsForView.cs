using BookStoreViewModels.ViewModels.Helpers;
using BookStoreViewModels.ViewModels.Products.BookItems;

namespace BookStoreViewModels.ViewModels.Products.DiscountCodes
{
    public class DiscountCodeDetailsForView : DiscountView
    {
        public string Code { get; set; }
        public bool IsAvailable { get; set; }
        public List<BookItemsForDiscountForView>? ListOfBookItems { get; set; }
    }
}
