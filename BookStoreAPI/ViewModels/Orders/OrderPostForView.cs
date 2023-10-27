using BookStoreAPI.Models.Orders;
using BookStoreAPI.ViewModels.Customers.Address;
using BookStoreAPI.ViewModels.Helpers;
using BookStoreAPI.ViewModels.Payments;
using BookStoreAPI.ViewModels.Shippings;

namespace BookStoreAPI.ViewModels.Orders
{
    public class OrderPostForView : BasePostView
    {
        public DateTime OrderDate { get; set; }
        public int? CustomerID { get; set; }
        public int? OrderStatusID { get; set; }
        public int? DeliveryMethodID { get; set; }
        public PaymentPostForView Payment { get; set; }
        public ShippingPostForView Shipping { get; set; }

        public List<ListOfIds>? ListOfOrderItems { get; set; }
    }
}
