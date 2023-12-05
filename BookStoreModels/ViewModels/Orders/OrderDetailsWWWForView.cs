using BookStoreViewModels.ViewModels.Helpers;
using BookStoreViewModels.ViewModels.Orders.Dictionaries;

namespace BookStoreViewModels.ViewModels.Orders
{
    public class OrderDetailsWWWForView : BaseView
    {
        public decimal FullBruttoPrice { get; set; }
        public List<OrderItemsWWWForView> orderItems { get; set; }
        public DeliveryMethodForView deliveryMethod { get; set; }

    }
}
