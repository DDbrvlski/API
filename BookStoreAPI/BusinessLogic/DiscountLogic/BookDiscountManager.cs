using BookStoreAPI.Helpers;
using BookStoreData.Data;
using BookStoreData.Models.Products.BookItems;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.BusinessLogic.DiscountLogic
{
    public class BookDiscountManager
    {
        public static async Task UpdateDiscounts(Discount discount, List<int?> bookItemIds, BookStoreContext _context)
        {
            var existingBookItemIds = await _context.BookDiscount
                .Where(x => x.DiscountID == discount.Id && x.IsActive == true)
                .Select(x => x.BookItemID)
                .ToListAsync();

            var bookItemsToDeactivate = existingBookItemIds.Except(bookItemIds).ToList();
            var bookItemsToAdd = bookItemIds.Except(existingBookItemIds).ToList();

            if (bookItemsToDeactivate.Count() > 0)
            {
                await DatabaseOperationHandler.HandleDatabaseOperation(
                    async () => await DeactivateChosenDiscounts(discount, bookItemsToDeactivate, _context),
                    "deaktywacji"
                );
            }

            if (bookItemsToAdd.Count() > 0)
            {
                await DatabaseOperationHandler.HandleDatabaseOperation(
                    async () => await AddNewDiscounts(discount, bookItemsToAdd, _context),
                    "dodawania"
                );
            }
        }
        public static async Task AddNewDiscounts(Discount discount, List<int?> bookItemIdsToAdd, BookStoreContext _context)
        {
            var bookItemsToAdd = bookItemIdsToAdd.Select(bookItemId => new BookDiscount
            {
                BookItemID = bookItemId,
                DiscountID = discount.Id,
            }).ToList();

            _context.BookDiscount.AddRange(bookItemsToAdd);

            await DatabaseOperationHandler.TryToSaveChangesAsync(_context);
        }
        public static async Task DeactivateAllDiscounts(Discount discount, BookStoreContext _context)
        {
            var discounts = await _context.BookDiscount
                .Where(x => x.DiscountID == discount.Id && x.IsActive == true)
                .ToListAsync();

            foreach (var bookDiscount in discounts)
            {
                bookDiscount.IsActive = false;
            }

            await DatabaseOperationHandler.TryToSaveChangesAsync(_context);
        }
        public static async Task DeactivateAllDiscounts(BookItem bookItem, BookStoreContext _context)
        {
            var discounts = await _context.BookDiscount
                .Where(x => x.BookItemID == bookItem.Id && x.IsActive == true)
                .ToListAsync();

            foreach (var bookDiscount in discounts)
            {
                bookDiscount.IsActive = false;
            }

            await DatabaseOperationHandler.TryToSaveChangesAsync(_context);
        }
        public static async Task DeactivateChosenDiscounts(Discount discount, List<int?> bookItemIdsToDeactivate, BookStoreContext _context)
        {
            var discountsToDeactivate = await _context.BookDiscount
                .Where(x => x.DiscountID == discount.Id && bookItemIdsToDeactivate.Contains(x.BookItemID) && x.IsActive == true)
                .ToListAsync();

            foreach (var discountItem in discountsToDeactivate)
            {
                discountItem.IsActive = false;
            }

            await DatabaseOperationHandler.TryToSaveChangesAsync(_context);
        }
    }
}
