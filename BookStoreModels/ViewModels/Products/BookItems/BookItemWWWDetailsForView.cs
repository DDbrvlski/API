using BookStoreViewModels.ViewModels.Helpers;
using BookStoreViewModels.ViewModels.Products.Books.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreViewModels.ViewModels.Products.BookItems
{
    public class BookItemWWWDetailsForView : BaseView
    {
        public string? BookTitle { get; set; }
        public string? FormName { get; set; }
        public double? Score { get; set; }
        public decimal? Price { get; set; }
        public string? FileFormatName { get; set; }
        public string? EditionName { get; set; }
        public string? PublisherName { get; set; }
        public string? Language { get; set; }
        public string? OriginalLanguage { get; set; }
        public string? TranslatorName { get; set; }
        public string? ISBN { get; set; }
        public string? Description { get; set; }
        public bool IsWishlisted { get; set; } = false;
        public DateTime ReleaseDate { get; set; }
        public List<AuthorsForView>? Authors { get; set; }
        public List<CategoryForView>? Categories { get; set; }
        public List<ImagesForView>? Images { get; set; }
        public Dictionary<int, int>? ScoreValues { get; set; }

    }
}
