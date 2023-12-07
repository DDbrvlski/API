using BookStoreViewModels.ViewModels.Helpers;

namespace BookStoreViewModels.ViewModels.Products.Books.Dictionaries
{
    public class ImagesForView : BaseView
    {
        public string? Title { get; set; }
        public string? ImageURL { get; set; }
        public int? Position { get; set; }
    }
}
