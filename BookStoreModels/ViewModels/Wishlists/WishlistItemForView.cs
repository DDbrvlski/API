using BookStoreViewModels.ViewModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreViewModels.ViewModels.Wishlists
{
    public class WishlistItemForView : BaseView
    {
        public string? FormName { get; set; }
        public string? BookTitle { get; set; }
        public string? EditionName { get; set; }
        public decimal? PriceBrutto { get; set; }
        public string? ImageURL { get; set; }
    }
}
