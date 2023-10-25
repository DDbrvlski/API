using BookStoreAPI.ViewModels.Helpers;

namespace BookStoreAPI.ViewModels.Products.StockAmount
{
    public class StockAmountForView : BaseView
    {
        public int? BookItemID { get; set; }
        public string BookTitle { get; set; }
    }
}
