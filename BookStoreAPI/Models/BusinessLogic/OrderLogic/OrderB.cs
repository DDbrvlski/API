using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Helpers.BaseBusinessLogic;
using BookStoreAPI.Models.BusinessLogic.DiscountCodeCodeLogic;
using BookStoreAPI.Models.Orders;
using BookStoreAPI.Models.Products.BookItems;
using BookStoreAPI.ViewModels.Orders;
using BookStoreAPI.ViewModels.Payments;
using BookStoreAPI.ViewModels.Shippings;

namespace BookStoreAPI.Models.BusinessLogic.OrderLogic
{
    public class OrderB : BaseBusinessLogic<Order, OrderPostForView>
    {
        protected override async Task AddNewEntityAsync(OrderPostForView entityWithData, BookStoreContext context)
        {
            Order newEntity = new Order();
            newEntity.CopyProperties(entityWithData);

            await ConvertListsToUpdate(newEntity, entityWithData, context);
        }
        protected override async Task ConvertListsToUpdate(Order entity, OrderPostForView entityWithData, BookStoreContext context)
        {
            List<int?> orderItemsIds = entityWithData.ListOfOrderItems.Select(x => x.Id).ToList();
            await UpdateAllConnectedEntitiesLists(entity, entityWithData.Payment, entityWithData.Shipping, orderItemsIds, context);
        }
        protected override async Task DeactivateAllConnectedEntities(Order entity, BookStoreContext context)
        {
            await OrderItemsManager.DeactivateAllItems(entity, context);
            await OrderPaymentManager.DeactivatePayment(entity, context);
            await OrderShippingManager.DeactivateShipping(entity, context);
        }
        private static async Task UpdateAllConnectedEntitiesLists(Order order, PaymentPostForView payment, ShippingPostForView shipping, List<int?> orderItemsIds, BookStoreContext context)
        {
            await OrderPaymentManager.UpdatePayment(order, payment, context);
            await OrderShippingManager.UpdateShipping(order, shipping, context);
            context.Order.Add(order);
            context.SaveChanges();
            await OrderItemsManager.UpdateItems(order, orderItemsIds, context);
        }
    }
}
