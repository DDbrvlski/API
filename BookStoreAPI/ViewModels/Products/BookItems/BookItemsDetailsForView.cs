using BookStoreAPI.ViewModels.Products.Books.Dictionaries;

namespace BookStoreAPI.ViewModels.Products.BookItems
{
    public class BookItemsDetailsForView
    {
        public float VAT { get; set; }
        public decimal NettoPrice { get; set; }
        public string ISBN { get; set; }
        public int Pages { get; set; }
        public DateTime PublishingDate { get; set; }
        public string TranslatorName { get; set; }
        public string LanguageName { get; set; }
        public string BookName { get; set; }
        public string EditionName { get; set; }
        public string FileFormatName { get; set; }
        public string FormName { get; set; }
        public string AvailabilityName { get; set; }
        public string BookTitle { get; set; }
        public string BookDescription { get; set; }
        public string BookOriginalLanguageName { get; set; }
        public string BookPublisherName { get; set; }
        public List<CategoryForView> BookCategories { get; set; } = new List<CategoryForView>();
        public List<AuthorsForView> BookAuthors { get; set; } = new List<AuthorsForView>();
    }
}
