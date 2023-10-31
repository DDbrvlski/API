using BookStoreAPI.ViewModels.Helpers;
using BookStoreAPI.ViewModels.Products.Books.Dictionaries;

namespace BookStoreAPI.ViewModels.Products.BookItems
{
    public class BookItemsForOrderDetailsForView : BaseView
    {
        public new int? Id { get; set; }
        public string Title { get; set; }
        public string ImageURL { get; set; }
        public List<AuthorsForView> AuthorsName { get; set; }
    }
}
