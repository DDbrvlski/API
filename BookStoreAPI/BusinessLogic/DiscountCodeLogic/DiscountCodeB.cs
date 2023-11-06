using BookStoreData.Data;
using BookStoreAPI.Helpers.BaseBusinessLogic;
using BookStoreData.Models.Products.BookItems;
using BookStoreViewModels.ViewModels.Products.DiscountCodes;

namespace BookStoreAPI.BusinessLogic.DiscountCodeLogic
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
