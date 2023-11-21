using BookStoreViewModels.ViewModels.Customers.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreViewModels.ViewModels.Accounts.Account
{
    public class CreateCustomerDataForView
    {
        public AddressPostForView? Address { get; set; }
        public AddressPostForView? MailingAddress { get; set; }
    }
}
