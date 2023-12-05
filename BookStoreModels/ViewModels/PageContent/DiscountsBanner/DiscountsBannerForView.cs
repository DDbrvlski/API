using BookStoreViewModels.ViewModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreViewModels.ViewModels.PageContent.DiscountsBanner
{
    public class DiscountsBannerForView : BaseView
    {
        public string? Header { get; set; }
        public string? ButtonTitle { get; set; }
        public string? ImageTitle { get; set; }
        public string? ImageURL { get; set; }
    }
}
