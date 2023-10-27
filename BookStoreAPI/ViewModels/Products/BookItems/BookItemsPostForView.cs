using BookStoreAPI.ViewModels.Helpers;

namespace BookStoreAPI.ViewModels.Products.BookItems
{
    public class BookItemsPostForView : BasePostView
    {
        public float VAT { get; set; }
        public decimal NettoPrice { get; set; }
        public string ISBN { get; set; }
        public int Pages { get; set; }
        public DateTime PublishingDate { get; set; }
        public int? TranslatorID { get; set; }
        public int? LanguageID { get; set; }
        public int? EditionID { get; set; }
        public int? FileFormatID { get; set; }
        public int? FormID { get; set; }
        public int? AvailabilityID { get; set; }
        public int? BookID { get; set; }
    }
}
