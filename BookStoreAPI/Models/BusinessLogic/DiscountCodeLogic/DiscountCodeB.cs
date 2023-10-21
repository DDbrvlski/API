using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Products.BookItems;
using BookStoreAPI.ViewModels.Products.DiscountCodes;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Models.BusinessLogic.DiscountCodeCodeLogic
{
    public class DiscountCodeB
    {
        public static async Task<IActionResult> ConvertDiscountCodePostForViewAndSave(DiscountCodePostForView discountCodeWithData, BookStoreContext _context)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    DiscountCode newDiscountCode = await AddNewDiscountCode(discountCodeWithData, _context);
                    await ConvertListsToUpdate(newDiscountCode, discountCodeWithData, _context);

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
        public static async Task<IActionResult> ConvertDiscountCodePostForViewAndUpdate(DiscountCode oldEntity, DiscountCodePostForView updatedEntity, BookStoreContext _context)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await UpdateDiscountCode(oldEntity, updatedEntity, _context);
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
        public static async Task<IActionResult> DeactivateDiscountCode(DiscountCode discountCode, BookStoreContext _context)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await DeactivateDiscountCodeCustom(discountCode, _context);
                    await DeactivateAllConnectedEntities(discountCode, _context);

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

        private static async Task ConvertListsToUpdate(DiscountCode discountCode, DiscountCodePostForView discountCodeWithData, BookStoreContext _context)
        {
            List<int?> bookItemsIds = discountCodeWithData.ListOfBookItems.Select(x => x.Id).ToList();
            await UpdateAllConnectedEntitiesLists(discountCode, bookItemsIds, _context);
        }

        private static async Task UpdateAllConnectedEntitiesLists(DiscountCode discountCode, List<int?> bookItemsIds, BookStoreContext _context)
        {
            await BookDiscountCodeManager.UpdateDiscountCodes(discountCode, bookItemsIds, _context);
        }
        private static async Task DeactivateAllConnectedEntities(DiscountCode discountCode, BookStoreContext _context)
        {
            await BookDiscountCodeManager.DeactivateAllDiscountCodes(discountCode, _context);
        }

        private static async Task DeactivateDiscountCodeCustom(DiscountCode discountCode, BookStoreContext _context)
        {
            discountCode.IsActive = false;
            await _context.SaveChangesAsync();
        }
        private static async Task<DiscountCode> AddNewDiscountCode(DiscountCodePostForView discountCodeWithData, BookStoreContext _context)
        {
            DiscountCode newDiscountCode = new DiscountCode();
            newDiscountCode.CopyProperties(discountCodeWithData);

            _context.DiscountCode.Add(newDiscountCode);
            await _context.SaveChangesAsync();

            return newDiscountCode;
        }
        private static async Task UpdateDiscountCode(DiscountCode oldEntity, DiscountCodePostForView updatedEntity, BookStoreContext _context)
        {
            oldEntity.CopyProperties(updatedEntity);
            await _context.SaveChangesAsync();
        }
    }
}
