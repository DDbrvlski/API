using BookStoreViewModels.ViewModels.Helpers;
using BookStoreViewModels.ViewModels.Products.Books.Dictionaries;

namespace BookStoreViewModels.ViewModels.Products.BookItems
{
    public class BookItemsWWWStoreForView : BaseView
    {
        public string? ImageURL { get; set; }
        public string? Title { get; set; }
        public decimal? Price { get; set; }
        public double Score { get; set; }
        public int? FormId { get; set; }
        public string? FormName { get; set; }
        public List<AuthorsForView> authors { get; set; } = new List<AuthorsForView>();

    }
}
