using BookStoreData.Data;
using BookStoreData.Models.Products.BookItems;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.BusinessLogic.DiscountCodeLogic
{
    public class BookDiscountCodeManager
    {
        public static async Task UpdateDiscountCodes(DiscountCode discountCode, List<int?> bookItemIds, BookStoreContext _context)
        {
            var existingBookItemIds = await _context.BookDiscountCode
                .Where(x => x.DiscountCodeID == discountCode.Id && x.IsActive == true)
                .Select(x => x.BookItemID)
                .ToListAsync();

            var bookItemsToDeactivate = existingBookItemIds.Except(bookItemIds).ToList();
            var bookItemsToAdd = bookItemIds.Except(existingBookItemIds).ToList();

            await DeactivateChosenDiscountCodes(discountCode, bookItemsToDeactivate, _context);
            await AddNewDiscountCodes(discountCode, bookItemsToAdd, _context);
        }
        public static async Task AddNewDiscountCodes(DiscountCode discountCode, List<int?> bookItemIdsToAdd, BookStoreContext _context)
        {
            var bookItemsToAdd = bookItemIdsToAdd.Select(bookItemId => new BookDiscountCode
            {
                BookItemID = bookItemId,
                DiscountCodeID = discountCode.Id,
            }).ToList();

            _context.BookDiscountCode.AddRange(bookItemsToAdd);
            await _context.SaveChangesAsync();
        }
        public static async Task DeactivateAllDiscountCodes(DiscountCode discountCode, BookStoreContext _context)
        {
            var discountCodes = await _context.BookDiscountCode
                .Where(x => x.DiscountCodeID == discountCode.Id && x.IsActive == true)
                .ToListAsync();

            foreach (var bookDiscountCode in discountCodes)
            {
                bookDiscountCode.IsActive = false;
            }

            await _context.SaveChangesAsync();
        }
        public static async Task DeactivateAllDiscountCodes(BookItem bookItem, BookStoreContext _context)
        {
            var discountCodes = await _context.BookDiscountCode
                .Where(x => x.BookItemID == bookItem.Id && x.IsActive == true)
                .ToListAsync();

            foreach (var bookDiscountCode in discountCodes)
            {
                bookDiscountCode.IsActive = false;
            }

            await _context.SaveChangesAsync();
        }
        public static async Task DeactivateChosenDiscountCodes(DiscountCode discountCode, List<int?> bookItemIdsToDeactivate, BookStoreContext _context)
        {
            var discountCodesToDeactivate = await _context.BookDiscountCode
                .Where(x => x.DiscountCodeID == discountCode.Id && bookItemIdsToDeactivate.Contains(x.BookItemID) && x.IsActive == true)
                .ToListAsync();

            foreach (var discountCodeItem in discountCodesToDeactivate)
            {
                discountCodeItem.IsActive = false;
            }

            await _context.SaveChangesAsync();
        }
    }
}
