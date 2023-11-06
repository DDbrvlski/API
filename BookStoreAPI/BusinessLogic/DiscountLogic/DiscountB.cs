using BookStoreData.Data;
using BookStoreAPI.Helpers.BaseBusinessLogic;
using BookStoreData.Models.Products.BookItems;
using BookStoreViewModels.ViewModels.Products.Discounts;

namespace BookStoreAPI.BusinessLogic.DiscountLogic
{
    public class DiscountB : BaseBusinessLogic<Discount, DiscountPostForView>
    {
        protected override async Task ConvertListsToUpdate(Discount entity, DiscountPostForView entityWithData, BookStoreContext context)
        {
            entity.StartingDate = entity.StartingDate.Date;
            entity.ExpiryDate = entity.ExpiryDate.Date;
            List<int?> bookItemsIds = entityWithData.ListOfBookItems.Select(x => x.Id).ToList();
            await UpdateAllConnectedEntitiesLists(entity, bookItemsIds, context);
        }
        protected override async Task DeactivateAllConnectedEntities(Discount entity, BookStoreContext context)
        {
            await BookDiscountManager.DeactivateAllDiscounts(entity, context);
        }
        private static async Task UpdateAllConnectedEntitiesLists(Discount discount, List<int?> bookItemsIds, BookStoreContext _context)
        {
            await BookDiscountManager.UpdateDiscounts(discount, bookItemsIds, _context);
        }
    }
}
