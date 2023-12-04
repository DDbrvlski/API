using BookStoreViewModels.ViewModels.Helpers;

namespace BookStoreViewModels.ViewModels.PageContent.FooterLinks
{
    public class FooterLinksFV
    {
        public int ColumnId { get; set; }
        public string ColumnName { get; set; }
        public int ColumnPosition { get; set; }
        public string HTMLObject { get; set; }

        public List<FooterLinksListDetailsForView> FooterLinksList { get; set; }
    }
}
