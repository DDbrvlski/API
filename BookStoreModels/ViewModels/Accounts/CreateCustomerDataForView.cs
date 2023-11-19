using BookStoreViewModels.ViewModels.Customers.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreViewModels.ViewModels.Accounts
{
    public class CreateCustomerDataForView
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int? GenderID { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public AddressPostForView? Address { get; set; }
        public AddressPostForView? MailingAddress { get; set; }
    }
}
