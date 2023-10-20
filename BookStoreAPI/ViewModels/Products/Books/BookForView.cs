using BookStoreAPI.ViewModels.Helpers;
using BookStoreAPI.ViewModels.Products.Books.Dictionaries;

namespace BookStoreAPI.ViewModels.Products.Books
{
    public class BookForView : BaseView
    {
        public string Title { get; set; }
        public string PublisherName { get; set; }
        public List<AuthorsForView> Authors { get; set; } = new List<AuthorsForView>();
    }
}
