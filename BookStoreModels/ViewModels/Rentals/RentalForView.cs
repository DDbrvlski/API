using BookStoreViewModels.ViewModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreViewModels.ViewModels.Rentals
{
    public class RentalForView : BaseView
    {
        public int? BookItemId { get; set; }
        public string? BookTitle { get; set; }
        public string? FileFormatName { get; set; }
        public string? ImageURL { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
