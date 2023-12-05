using BookStoreViewModels.ViewModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreViewModels.ViewModels.Products.Books
{
    public class BookReviewPostWWWForView : BaseView
    {
        public string? Content { get; set; }
        public int? ScoreId { get; set; }
        public int? BookItemId { get; set; }
    }
}
