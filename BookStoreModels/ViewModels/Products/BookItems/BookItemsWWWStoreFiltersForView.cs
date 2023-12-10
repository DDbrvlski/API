using BookStoreViewModels.ViewModels.Helpers;

namespace BookStoreViewModels.ViewModels.Products.BookItems
{
    public class BookItemsWWWStoreFiltersForView
    {
        public string? searchPhrase { get; set; }
        public List<int?>? authorIds { get; set; }
        public List<int?>? categoryIds { get; set; }
        public List<int?>? formIds { get; set; }
        public List<int?>? publisherIds { get; set; }
        public List<int?>? languageIds { get; set; }
        public List<int?>? scoreValues { get; set; }
        public List<int?>? availabilitiesIds { get; set; }
        public decimal? priceFrom { get; set; }
        public decimal? priceTo { get; set; }
        public bool? isOnSale { get; set; }
        public string? sortBy { get; set; }
        public string? sortOrder { get; set;}
        public int? numberOfElements { get; set; }
        public int? bookId { get; set; }
    }
}
