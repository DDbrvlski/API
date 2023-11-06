using BookStoreViewModels.ViewModels.Helpers;
using BookStoreViewModels.ViewModels.Payments;
using BookStoreViewModels.ViewModels.Shippings;

namespace BookStoreViewModels.ViewModels.Orders
{
    public class OrderPostForView : BasePostView
    {
        public DateTime OrderDate { get; set; }
        public int? CustomerID { get; set; }
        public int? OrderStatusID { get; set; }
        public int? DeliveryMethodID { get; set; }
        public PaymentPostForView Payment { get; set; }
        public ShippingPostForView Shipping { get; set; }

        public List<ListOfOrderItemsIds>? ListOfOrderItems { get; set; }
    }
}
