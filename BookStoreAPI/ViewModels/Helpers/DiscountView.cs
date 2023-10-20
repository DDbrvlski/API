namespace BookStoreAPI.ViewModels.Helpers
{
    public class DiscountView : BaseView
    {
        public decimal PercentOfDiscount { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime StartingDate { get; set; }
        public string Description { get; set; }
    }
}
