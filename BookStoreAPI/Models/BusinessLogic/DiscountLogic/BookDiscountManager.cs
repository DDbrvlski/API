using BookStoreAPI.Data;
using BookStoreAPI.Models.Media;
using BookStoreAPI.Models.Products.BookItems;
using BookStoreAPI.Models.Products.Books;
using BookStoreAPI.ViewModels.Products.Books.Dictionaries;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Models.BusinessLogic.DiscountLogic
{
    public class BookDiscountManager
    {
        public static async Task UpdateDiscounts(Discount discount, List<int?> bookItemIds, BookStoreContext _context)
        {
            var existingBookItemIds = await _context.BookDiscount
                .Where(x => x.DiscountID == discount.Id)
                .Select(x => x.BookItemID)
                .ToListAsync();

            var bookItemsToDeactivate = existingBookItemIds.Except(bookItemIds).ToList();
            var bookItemsToAdd = bookItemIds.Except(existingBookItemIds).ToList();

            await DeactivateChosenDiscounts(discount, bookItemsToDeactivate, _context);
            await AddNewDiscounts(discount, bookItemsToAdd, _context);
        }
        public static async Task AddNewDiscounts(Discount discount, List<int?> bookItemIdsToAdd, BookStoreContext _context)
        {
            var bookItemsToAdd = bookItemIdsToAdd.Select(bookItemId => new BookDiscount
            {
                BookItemID = bookItemId,
                DiscountID = discount.Id,
            }).ToList();

            _context.BookDiscount.AddRange(bookItemsToAdd);
            await _context.SaveChangesAsync();
        }
        public static async Task DeactivateAllDiscounts(Discount discount, BookStoreContext _context)
        {
            var discounts = await _context.BookDiscount
                .Where(x => x.DiscountID == discount.Id)
                .ToListAsync();

            foreach (var bookDiscount in discounts)
            {
                bookDiscount.IsActive = false;
            }

            await _context.SaveChangesAsync();
        }
        public static async Task DeactivateChosenDiscounts(Discount discount, List<int?> bookItemIdsToDeactivate, BookStoreContext _context)
        {
            var discountsToDeactivate = await _context.BookDiscount
                .Where(x => x.DiscountID == discount.Id && bookItemIdsToDeactivate.Contains(x.BookItemID))
                .ToListAsync();

            foreach (var discountItem in discountsToDeactivate)
            {
                discountItem.IsActive = false;
            }

            await _context.SaveChangesAsync();
        }
    }
}
