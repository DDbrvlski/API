using BookStoreAPI.ViewModels.Customers.Address;
using BookStoreAPI.ViewModels.Helpers;
using System.Net.Sockets;

namespace BookStoreAPI.ViewModels.Customers
{
    public class CustomerDetailsForView : BaseView
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsSubscribed { get; set; }
        public int GenderID { get; set; }
        public string GenderName { get; set; }
        public List<AddressForView>? ListOfCustomerAdresses { get; set; }
    }
}
