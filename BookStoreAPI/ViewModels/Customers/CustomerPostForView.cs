﻿using BookStoreAPI.ViewModels.Customers.Address;
using BookStoreAPI.ViewModels.Helpers;

namespace BookStoreAPI.ViewModels.Customers
{
    public class CustomerPostForView : BaseView
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public bool IsSubscribed { get; set; }
        public int GenderID { get; set; }
        public List<AddressPostForView>? ListOfCustomerAdresses { get; set; }
    }
}