using BookStoreViewModels.ViewModels.Helpers;
using BookStoreViewModels.ViewModels.Products.Books.Dictionaries;

namespace BookStoreViewModels.ViewModels.Products.Books
{
    public class BookForView : BaseView
    {
        public string Title { get; set; }
        public string PublisherName { get; set; }
        public List<AuthorsForView> Authors { get; set; } = new List<AuthorsForView>();
    }
}
