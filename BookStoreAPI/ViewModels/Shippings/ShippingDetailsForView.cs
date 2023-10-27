using BookStoreAPI.ViewModels.Customers.Address;
using BookStoreAPI.ViewModels.Helpers;
using BookStoreAPI.ViewModels.Shippings.Dictionaries;

namespace BookStoreAPI.ViewModels.Shippings
{
    public class ShippingDetailsForView : BaseView
    {
        public DateTime? ShippingDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public AddressDetailsForView ShippingAddress { get; set; }
        public ShippingStatusForView ShippingStatus { get; set; }
    }
}
