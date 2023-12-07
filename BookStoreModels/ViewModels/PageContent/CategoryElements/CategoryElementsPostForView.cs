using BookStoreViewModels.ViewModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreViewModels.ViewModels.PageContent.CategoryElements
{
    public class CategoryElementsPostForView : BaseView
    {
        public string? Path { get; set; }
        public string? Logo { get; set; }
        public string? Content { get; set; }
        public int? Position { get; set; }
        public string? ImageTitle { get; set; }
        public string? ImageURL { get; set; }
        public int? CategoryID { get; set; }
    }
}
