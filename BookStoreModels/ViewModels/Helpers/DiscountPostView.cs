namespace BookStoreViewModels.ViewModels.Helpers
{
    public class DiscountPostView : BasePostView
    {
        public decimal PercentOfDiscount { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime StartingDate { get; set; }
        public string Description { get; set; }
    }
}
