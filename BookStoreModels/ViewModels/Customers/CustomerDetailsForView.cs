using BookStoreViewModels.ViewModels.Customers.Address;
using BookStoreViewModels.ViewModels.Helpers;

namespace BookStoreViewModels.ViewModels.Customers
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
        public List<AddressDetailsForView>? ListOfCustomerAdresses { get; set; }
    }
}
