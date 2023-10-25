using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Helpers.BaseBusinessLogic;
using BookStoreAPI.Models.Products.BookItems;
using BookStoreAPI.ViewModels.Products.DiscountCodes;
using BookStoreAPI.ViewModels.Products.Discounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Models.BusinessLogic.DiscountCodeCodeLogic
{
    public class DiscountCodeB : BaseBusinessLogic<DiscountCode, DiscountCodePostForView>
    {
        protected override async Task ConvertListsToUpdate(DiscountCode entity, DiscountCodePostForView entityWithData, BookStoreContext context)
        {
            entity.StartingDate = entity.StartingDate.Date;
            entity.ExpiryDate = entity.ExpiryDate.Date;
            List<int?> bookItemsIds = entityWithData.ListOfBookItems.Select(x => x.Id).ToList();
            await UpdateAllConnectedEntitiesLists(entity, bookItemsIds, context);
        }
        protected override async Task DeactivateAllConnectedEntities(DiscountCode entity, BookStoreContext context)
        {
            await BookDiscountCodeManager.DeactivateAllDiscountCodes(entity, context);
        }
        private static async Task UpdateAllConnectedEntitiesLists(DiscountCode discountCode, List<int?> bookItemsIds, BookStoreContext _context)
        {
            await BookDiscountCodeManager.UpdateDiscountCodes(discountCode, bookItemsIds, _context);
        }

    }
}
