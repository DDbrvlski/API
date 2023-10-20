using BookStoreAPI.ViewModels.Helpers;

namespace BookStoreAPI.ViewModels.Products.Discounts
{
    public class DiscountPostForView : BaseView
    {
        public string Title { get; set; }
        public decimal PercentOfDiscount { get; set; }
        public bool Valid { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime StartingDate { get; set; }
        public string Description { get; set; }
        public List<ListOfIds>? ListOfBookItems { get; set; }

    }
}
