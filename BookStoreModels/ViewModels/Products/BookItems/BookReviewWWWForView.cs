using BookStoreViewModels.ViewModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreViewModels.ViewModels.Products.BookItems
{
    public class BookReviewWWWForView : BaseView
    {
        public string? CustomerName { get; set; }
        public int? ScoreValue { get; set; }
        public DateTime CreationDate { get; set; }
        public string? Content { get; set; }
    }
}
