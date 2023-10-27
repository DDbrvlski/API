using BookStoreAPI.ViewModels.Helpers;

namespace BookStoreAPI.ViewModels.Customers.Address
{
    public class AddressDetailsForView : BaseAddressView
    {
        public string CityName { get; set; }
        public string CountryName { get; set; }
    }
}