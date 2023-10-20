using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.BusinessLogic.BookLogic;
using BookStoreAPI.Models.Media;
using BookStoreAPI.Models.Products.BookItems;
using BookStoreAPI.Models.Products.Books;
using BookStoreAPI.ViewModels.Products.BookItems;
using BookStoreAPI.ViewModels.Products.Books.Dictionaries;
using BookStoreAPI.ViewModels.Products.Discounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Models.BusinessLogic.DiscountLogic
{
    public class DiscountB
    {
        public static async Task<IActionResult> ConvertDiscountPostForViewAndSave(DiscountPostForView discountWithData, BookStoreContext _context)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    Discount newDiscount = await AddNewDiscount(discountWithData, _context);
                    await ConvertListsToUpdate(newDiscount, discountWithData, _context);

                    transaction.Commit();
                    return new OkResult();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new BadRequestObjectResult(ex.Message);
                }
            }
        }
        public static async Task<IActionResult> ConvertDiscountPostForViewAndUpdate(Discount oldEntity, DiscountPostForView updatedEntity, BookStoreContext _context)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await UpdateDiscount(oldEntity, updatedEntity, _context);
                    await ConvertListsToUpdate(oldEntity, updatedEntity, _context);

                    transaction.Commit();
                    return new OkResult();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new BadRequestObjectResult(ex.Message);
                }
            }
        }
        public static async Task<IActionResult> DeactivateDiscount(Discount discount, BookStoreContext _context)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await DeactivateDiscountCustom(discount, _context);
                    await DeactivateAllConnectedEntities(discount, _context);

                    transaction.Commit();
                    return new OkResult();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new BadRequestObjectResult(ex.Message);
                }
            }
        }

        private static async Task ConvertListsToUpdate(Discount discount, DiscountPostForView discountWithData, BookStoreContext _context)
        {
            List<int?> bookItemsIds = discountWithData.ListOfBookItems.Select(x => x.Id).ToList();
            await UpdateAllConnectedEntitiesLists(discount, bookItemsIds, _context);
        }

        private static async Task UpdateAllConnectedEntitiesLists(Discount discount, List<int?> bookItemsIds, BookStoreContext _context)
        {
            await BookDiscountManager.UpdateDiscounts(discount, bookItemsIds, _context);
        }
        private static async Task DeactivateAllConnectedEntities(Discount discount, BookStoreContext _context)
        {
            await BookDiscountManager.DeactivateAllDiscounts(discount, _context);
        }

        private static async Task DeactivateDiscountCustom(Discount discount, BookStoreContext _context)
        {
            discount.IsActive = false;
            await _context.SaveChangesAsync();
        }
        private static async Task<Discount> AddNewDiscount(DiscountPostForView discountWithData, BookStoreContext _context)
        {
            Discount newDiscount = new Discount();
            newDiscount.CopyProperties(discountWithData);

            _context.Discount.Add(newDiscount);
            await _context.SaveChangesAsync();

            return newDiscount;
        }
        private static async Task UpdateDiscount(Discount oldEntity, DiscountPostForView updatedEntity, BookStoreContext _context)
        {
            oldEntity.CopyProperties(updatedEntity);
            await _context.SaveChangesAsync();
        }

        
    }
}
