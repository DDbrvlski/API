using BookStoreViewModels.ViewModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreViewModels.ViewModels.Products.BookItems
{
    public class BookItemsCarouselForView : BaseView
    {
        public string? ImgURL { get; set; }
        public string? Title { get; set; }
        public int? FormId { get; set; }
        public string? FormName { get; set; }
    }
}
