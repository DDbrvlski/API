using BookStoreAPI.ViewModels.Helpers;
using BookStoreAPI.ViewModels.Products.BookItems;

namespace BookStoreAPI.ViewModels.Orders
{
    public class OrderItemsDetailsForView : BaseView
    {
        public int Quantity { get; set; }
        public decimal BruttoPrice { get; set; }
        public int? BookItemID { get; set; }
        public int? OrderID { get; set; }
        public BookItemsForOrderDetailsForView ItemForOrder { get; set; }
    }
}
