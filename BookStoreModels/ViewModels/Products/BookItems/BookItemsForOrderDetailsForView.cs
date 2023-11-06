using BookStoreViewModels.ViewModels.Helpers;
using BookStoreViewModels.ViewModels.Products.Books.Dictionaries;

namespace BookStoreViewModels.ViewModels.Products.BookItems
{
    public class BookItemsForOrderDetailsForView : BaseView
    {
        public new int? Id { get; set; }
        public string Title { get; set; }
        public string ImageURL { get; set; }
        public List<AuthorsForView> AuthorsName { get; set; }
    }
}
