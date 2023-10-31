using BookStoreAPI.Data;
using BookStoreAPI.Models.BusinessLogic.BookLogic;
using BookStoreAPI.Models.Media;
using BookStoreAPI.Models.Orders;
using BookStoreAPI.Models.Products.Books;
using BookStoreAPI.ViewModels.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Models.BusinessLogic.OrderLogic
{
    public class OrderItemsManager
    {
        public static async Task UpdateItems(Order order, List<ListOfOrderItemsIds?> orderItems, BookStoreContext _context)
        {
            var existingOrderItemsIds = await _context.OrderItems
                .Where(x => x.OrderID == order.Id && x.IsActive == true)
                .Select(x => x.BookItemID)
                .ToListAsync();

            var orderItemsIds = orderItems.Select(x => (int?)x.Id).ToList();

            var orderItemsToDeactivate = existingOrderItemsIds.Except(orderItemsIds).ToList();
            var orderItemsToAdd = orderItems.Where(x => x != null && !existingOrderItemsIds.Contains(x.Id)).ToList();

            await DeactivateChosenItems(order, orderItemsToDeactivate, _context);
            await AddNewItems(order, orderItemsToAdd, _context);
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
            await _context.SaveChangesAsync();
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

            await _context.SaveChangesAsync();
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

            await _context.SaveChangesAsync();
        }
    }
}
