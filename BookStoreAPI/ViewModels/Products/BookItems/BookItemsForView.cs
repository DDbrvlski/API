using BookStoreAPI.ViewModels.Helpers;

namespace BookStoreAPI.ViewModels.Products.BookItems
{
    public class BookItemsForView : BaseView
    {
        public decimal NettoPrice { get; set; }
        public string ISBN { get; set; }
        public string FormName { get; set; }
        public int? BookID { get; set; }
        public string BookTitle { get; set; }
    }
}
