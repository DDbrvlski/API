using BookStoreAPI.Models.Orders;
using BookStoreAPI.ViewModels.Customers;
using BookStoreAPI.ViewModels.Helpers;
using BookStoreAPI.ViewModels.Orders.Dictionaries;
using BookStoreAPI.ViewModels.Payments;
using BookStoreAPI.ViewModels.Shippings;

namespace BookStoreAPI.ViewModels.Orders
{
    public class OrderDetailsForView : BaseView
    {
        public DateTime OrderDate { get; set; }
        public decimal Amount { get; set; }
        public DeliveryMethodForView DeliveryMethod { get; set; }
        public OrderStatusForView OrderStatus { get; set; }
        public PaymentDetailsForView Payment { get; set; }
        public ShippingDetailsForView Shipping { get; set; }
        public CustomerForOrderForView Customer { get; set; }

        public List<OrderItems>? ListOfOrderItems { get; set; }
    }
}
