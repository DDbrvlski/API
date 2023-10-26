using BookStoreAPI.ViewModels.Helpers;
using BookStoreAPI.ViewModels.Products.BookItems;

namespace BookStoreAPI.ViewModels.Products.DiscountCodes
{
    public class DiscountCodeDetailsForView : DiscountView
    {
        public string Code { get; set; }
        public bool IsAvailable { get; set; }
        public List<BookItemsForDiscountForView>? ListOfBookItems { get; set; }
    }
}
