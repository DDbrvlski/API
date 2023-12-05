using BookStoreViewModels.ViewModels.Customers.Address;
using BookStoreViewModels.ViewModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreViewModels.ViewModels.Accounts.User
{
    public class UserAddressForView
    {
        public BaseAddressView? address {  get; set; }
        public BaseAddressView? mailingAddress {  get; set; }
    }
}
