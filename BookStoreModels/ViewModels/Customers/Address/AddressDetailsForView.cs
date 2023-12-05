using BookStoreViewModels.ViewModels.Helpers;

namespace BookStoreViewModels.ViewModels.Customers.Address
{
    public class AddressDetailsForView : BaseAddressView
    {
        public string? CityName { get; set; }
        public string? CountryName { get; set; }
    }
}