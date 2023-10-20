using BookStoreAPI.ViewModels.Helpers;
using BookStoreAPI.ViewModels.Products.BookItems;

namespace BookStoreAPI.ViewModels.Products.Discounts
{
    public class DiscountDetailsForView : BaseView
    {
        public string Title { get; set; }
        public decimal PercentOfDiscount { get; set; }
        public bool Valid { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime StartingDate { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        public List<BookItemsForView>? ListOfBookItems { get; set; }
    }
}
