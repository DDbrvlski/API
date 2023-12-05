using BookStoreViewModels.ViewModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreViewModels.ViewModels.Products.Books
{
    public class BookAllReviewsWWWForView
    {
        public double BookItemScore { get; set; }
        public Dictionary<int, int> ScoreValues { get; set; }
        public List<BookReviewWWWForView> BookReviews { get; set; }
    }
}
