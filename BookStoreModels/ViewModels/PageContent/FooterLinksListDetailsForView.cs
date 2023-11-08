using BookStoreViewModels.ViewModels.Helpers;

namespace BookStoreViewModels.ViewModels.PageContent
{
    public class FooterLinksListDetailsForView : BaseView
    {
        public string Name { get; set; }
        public string? Path { get; set; }
        public string? URL { get; set; }
        public int? Position { get; set; }
    }
}
