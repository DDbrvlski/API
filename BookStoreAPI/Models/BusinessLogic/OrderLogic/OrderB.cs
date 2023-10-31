﻿using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Helpers.BaseBusinessLogic;
using BookStoreAPI.Models.BusinessLogic.DiscountCodeCodeLogic;
using BookStoreAPI.Models.Orders;
using BookStoreAPI.Models.Products.BookItems;
using BookStoreAPI.ViewModels.Helpers;
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
            List<ListOfOrderItemsIds?> orderItemsIds = entityWithData.ListOfOrderItems.ToList();
            await UpdateAllConnectedEntitiesLists(entity, entityWithData.Payment, entityWithData.Shipping, orderItemsIds, context);
        }
        protected override async Task DeactivateAllConnectedEntities(Order entity, BookStoreContext context)
        {
            await OrderItemsManager.DeactivateAllItems(entity, context);
            await OrderPaymentManager.DeactivatePayment(entity, context);
            await OrderShippingManager.DeactivateShipping(entity, context);
            CalculateTotalPriceForOrder(entity, context);
        }
        private static async Task UpdateAllConnectedEntitiesLists(Order order, PaymentPostForView payment, ShippingPostForView shipping, List<ListOfOrderItemsIds?> orderItemsIds, BookStoreContext context)
        {
            await OrderPaymentManager.UpdatePayment(order, payment, context);
            await OrderShippingManager.UpdateShipping(order, shipping, context);
            context.Order.Add(order);
            context.SaveChanges();
            await OrderItemsManager.UpdateItems(order, orderItemsIds, context);
            await CalculateTotalPriceForOrder(order, context);
        }
        private static async Task CalculateTotalPriceForOrder(Order order, BookStoreContext context)
        {
            var orderItemsPrice = context.OrderItems.Where(x => x.OrderID == order.Id && x.IsActive == true).Select(x => x.BruttoPrice).Sum();
            var paymentToEdit = context.Payment.First(x => x.Id == order.PaymentID);
            var deliveryMethodPrice = context.DeliveryMethod.First(x => x.Id == order.DeliveryMethodID);

            paymentToEdit.Amount = orderItemsPrice + order.DeliveryMethod.Price;

            context.SaveChanges();
        }
    }
}
