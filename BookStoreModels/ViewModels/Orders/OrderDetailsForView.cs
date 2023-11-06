using BookStoreViewModels.ViewModels.Customers;
using BookStoreViewModels.ViewModels.Helpers;
using BookStoreViewModels.ViewModels.Orders.Dictionaries;
using BookStoreViewModels.ViewModels.Payments;
using BookStoreViewModels.ViewModels.Shippings;

namespace BookStoreViewModels.ViewModels.Orders
{
    public class OrderDetailsForView : BaseView
    {
        public DateTime OrderDate { get; set; }
        public DeliveryMethodForView DeliveryMethod { get; set; }
        public OrderStatusForView OrderStatus { get; set; }
        public PaymentDetailsForView Payment { get; set; }
        public ShippingDetailsForView Shipping { get; set; }
        public CustomerForOrderForView Customer { get; set; }

        public List<OrderItemsDetailsForView>? ListOfOrderItems { get; set; }
    }
}
