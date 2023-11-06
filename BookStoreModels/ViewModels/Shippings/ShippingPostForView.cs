using BookStoreViewModels.ViewModels.Customers.Address;
using BookStoreViewModels.ViewModels.Helpers;
using BookStoreViewModels.ViewModels.Shippings.Dictionaries;

namespace BookStoreViewModels.ViewModels.Shippings
{
    public class ShippingPostForView : BasePostView
    {
        public DateTime? ShippingDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public AddressPostForView Address { get; set; }
        public ShippingStatusForView ShippingStatus { get; set; }
    }
}
