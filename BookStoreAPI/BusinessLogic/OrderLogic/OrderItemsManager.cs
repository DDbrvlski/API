using BookStoreAPI.Helpers;
using BookStoreData.Data;
using BookStoreData.Models.Orders;
using BookStoreData.Models.Products.Books;
using BookStoreViewModels.ViewModels.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.BusinessLogic.OrderLogic
{
    public class OrderItemsManager
    {
        public static async Task UpdateItems(Order order, List<ListOfOrderItemsIds?> orderItems, BookStoreContext _context)
        {
            var existingOrderItemsIds = await _context.OrderItems
                .Where(x => x.OrderID == order.Id && x.IsActive == true)
                .Select(x => x.BookItemID)
                .ToListAsync();

            var orderItemsIds = orderItems.Select(x => x.Id).ToList();

            var orderItemsToDeactivate = existingOrderItemsIds.Except(orderItemsIds).ToList();
            var orderItemsToAdd = orderItems.Where(x => x != null && !existingOrderItemsIds.Contains(x.Id)).ToList();

            if (orderItemsToDeactivate.Count() > 0)
            {
                await DatabaseOperationHandler.HandleDatabaseOperation(
                    async () => await DeactivateChosenItems(order, orderItemsToDeactivate, _context),
                    "deaktywacji"
                );
            }

            if (orderItemsToAdd.Count() > 0)
            {
                await DatabaseOperationHandler.HandleDatabaseOperation(
                    async () => await AddNewItems(order, orderItemsToAdd, _context),
                    "dodawania"
                );
            }
        }

        public static async Task AddNewItems(Order order, List<ListOfOrderItemsIds?> orderItemsToAdd, BookStoreContext _context)
        {
            var itemsToAdd = orderItemsToAdd.Select(itemId => new OrderItems
            {
                BookItemID = itemId.Id,
                OrderID = order.Id,
                Quantity = itemId.Quantity,
                BruttoPrice = itemId.BruttoPrice
            }).ToList();

            _context.OrderItems.AddRange(itemsToAdd);

            await DatabaseOperationHandler.TryToSaveChangesAsync(_context);
        }

        public static async Task DeactivateAllItems(Order order, BookStoreContext _context)
        {
            var items = await _context.OrderItems
                .Where(x => x.OrderID == order.Id && x.IsActive == true)
                .ToListAsync();

            foreach (var item in items)
            {
                item.IsActive = false;
            }

            await DatabaseOperationHandler.TryToSaveChangesAsync(_context);
        }

        public static async Task DeactivateChosenItems(Order order, List<int?> orderItemsToDeactivate, BookStoreContext _context)
        {
            var itemsToDeactivate = await _context.OrderItems
                .Where(x => x.OrderID == order.Id && orderItemsToDeactivate.Contains(x.BookItemID) && x.IsActive == true)
                .ToListAsync();

            foreach (var item in itemsToDeactivate)
            {
                item.IsActive = false;
            }

            await DatabaseOperationHandler.TryToSaveChangesAsync(_context);
        }
    }
}
