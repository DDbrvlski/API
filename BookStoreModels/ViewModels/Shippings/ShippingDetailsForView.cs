using BookStoreViewModels.ViewModels.Customers.Address;
using BookStoreViewModels.ViewModels.Helpers;
using BookStoreViewModels.ViewModels.Shippings.Dictionaries;

namespace BookStoreViewModels.ViewModels.Shippings
{
    public class ShippingDetailsForView : BaseView
    {
        public DateTime? ShippingDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public AddressDetailsForView ShippingAddress { get; set; }
        public ShippingStatusForView ShippingStatus { get; set; }
    }
}
