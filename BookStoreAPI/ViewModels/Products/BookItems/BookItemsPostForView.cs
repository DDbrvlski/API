using BookStoreAPI.ViewModels.Helpers;

namespace BookStoreAPI.ViewModels.Products.BookItems
{
    public class BookItemsPostForView : BaseView
    {
        public float VAT { get; set; }
        public decimal NettoPrice { get; set; }
        public string ISBN { get; set; }
        public int Pages { get; set; }
        public DateTime PublishingDate { get; set; }
        public int TranslatorId { get; set; }
        public int LanguageId { get; set; }
        public int EditionId { get; set; }
        public int FileFormatId { get; set; }
        public int FormId { get; set; }
        public int AvailabilityId { get; set; }
        public int BookId { get; set; }
    }
}
