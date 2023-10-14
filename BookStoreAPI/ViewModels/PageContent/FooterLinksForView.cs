using BookStoreAPI.Models.Helpers;

namespace BookStoreAPI.ViewModels.PageContent
{
    public class FooterLinksForView : BaseEntity
    {
        public string Name { get; set; }
        public int ColumnId { get; set; }
        public string ColumnName { get; set; }
        public int ColumnPosition { get; set; }
        public string HTMLObject { get; set; }
    }
}
