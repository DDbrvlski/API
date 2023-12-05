using BookStoreAPI.Helpers;
using BookStoreAPI.Helpers.BaseBusinessLogic;
using BookStoreData.Data;
using BookStoreData.Models.Customers;
using BookStoreData.Models.Orders;
using BookStoreViewModels.ViewModels.Helpers;
using BookStoreViewModels.ViewModels.Orders;
using BookStoreViewModels.ViewModels.Orders.Dictionaries;
using BookStoreViewModels.ViewModels.Payments;
using BookStoreViewModels.ViewModels.Shippings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.BusinessLogic.OrderLogic
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
            await CalculateTotalPriceForOrder(entity, context);
        }
        public static async Task<ActionResult<IEnumerable<OrderDetailsWWWForView>>> GetUserOrders(Customer customer, BookStoreContext context)
        {
            return await context.Order
                .Include(x => x.OrderItems)
                    .ThenInclude(x => x.BookItem)
                    .ThenInclude(x => x.Book)
                .Include(x => x.OrderItems)
                    .ThenInclude(x => x.BookItem)
                    .ThenInclude(x => x.Form)
                .Include(x => x.OrderItems)
                    .ThenInclude(x => x.BookItem)
                    .ThenInclude(x => x.Edition)
                .Include(x => x.OrderItems)
                    .ThenInclude(x => x.BookItem)
                    .ThenInclude(x => x.Book)
                    .ThenInclude(x => x.BookImages)
                    .ThenInclude(x => x.Image)
                .Include(x => x.DeliveryMethod)
                .Where(x => x.CustomerID == customer.Id)
                .OrderByDescending(x => x.Id)
                .Select(x => new OrderDetailsWWWForView()
                {
                    Id = x.Id,
                    deliveryMethod = new DeliveryMethodForView()
                    {
                        Id = x.DeliveryMethod.Id,
                        Name = x.DeliveryMethod.Name,
                        Price = x.DeliveryMethod.Price
                    },
                    orderItems = x.OrderItems.Select(y => new OrderItemsWWWForView()
                    {
                        Id = (int)y.BookItemID,
                        BookTitle = y.BookItem.Book.Title,
                        EditionName = y.BookItem.Edition.Name,
                        FormName = y.BookItem.Form.Name,
                        ImageURL = y.BookItem.Book.BookImages.OrderBy(x => x.Image.Position).First().Image.ImageURL,
                        Quantity = y.Quantity,
                        PriceBrutto = y.BruttoPrice,
                        FullPriceBrutto = y.BruttoPrice * (decimal)y.Quantity,
                    }).ToList(),
                    FullBruttoPrice = x.DeliveryMethod.Price + x.OrderItems.Sum(x => x.BruttoPrice),
                }).ToListAsync();
        } 
        private static async Task UpdateAllConnectedEntitiesLists(Order order, PaymentPostForView payment, ShippingPostForView shipping, List<ListOfOrderItemsIds?> orderItemsIds, BookStoreContext context)
        {
            await OrderPaymentManager.UpdatePayment(order, payment, context);
            await OrderShippingManager.UpdateShipping(order, shipping, context);
            context.Order.Add(order);
            await DatabaseOperationHandler.TryToSaveChangesAsync(context);
            await OrderItemsManager.UpdateItems(order, orderItemsIds, context);
            await CalculateTotalPriceForOrder(order, context);
        }
        private static async Task CalculateTotalPriceForOrder(Order order, BookStoreContext context)
        {
            var orderItemsPrice = context.OrderItems.Where(x => x.OrderID == order.Id && x.IsActive == true).Select(x => x.BruttoPrice).Sum();
            var paymentToEdit = context.Payment.First(x => x.Id == order.PaymentID);
            var deliveryMethodPrice = context.DeliveryMethod.First(x => x.Id == order.DeliveryMethodID);

            paymentToEdit.Amount = orderItemsPrice + order.DeliveryMethod.Price;

            await DatabaseOperationHandler.TryToSaveChangesAsync(context);
        }
    }
}
