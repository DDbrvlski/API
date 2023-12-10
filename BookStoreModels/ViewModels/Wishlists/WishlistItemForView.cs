using BookStoreViewModels.ViewModels.Helpers;
using BookStoreViewModels.ViewModels.Products.Books.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreViewModels.ViewModels.Wishlists
{
    public class WishlistItemForView : BaseView
    {
        public int? FormId { get; set; }
        public string? FormName { get; set; }
        public string? FileFormatName { get; set; }
        public string? BookTitle { get; set; }
        public string? EditionName { get; set; }
        public decimal? PriceBrutto { get; set; }
        public string? ImageURL { get; set; }
        public List<AuthorsForView> authors { get; set; } = new List<AuthorsForView>();
    }
}
