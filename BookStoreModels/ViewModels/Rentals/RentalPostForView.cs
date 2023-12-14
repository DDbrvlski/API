using BookStoreViewModels.ViewModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreViewModels.ViewModels.Rentals
{
    public class RentalPostForView : BaseView
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? BookItemID { get; set; }
        public int? PaymentMethodID { get; set; }
        public int? RentalTypeID { get; set; }
        public int? RentalStatusID { get; set; }
    }
}
