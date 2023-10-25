using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Helpers.BaseBusinessLogic;
using BookStoreAPI.Interfaces;
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
