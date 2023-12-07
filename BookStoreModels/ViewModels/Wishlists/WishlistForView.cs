using BookStoreViewModels.ViewModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreViewModels.ViewModels.Wishlists
{
    public class WishlistForView : BaseView
    {
        public decimal? FullPrice { get; set; }
        public List<WishlistItemForView>? Items { get; set; }
    }
}
