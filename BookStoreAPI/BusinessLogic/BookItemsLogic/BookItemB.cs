using BookStoreAPI.BusinessLogic.DiscountCodeLogic;
using BookStoreAPI.BusinessLogic.DiscountLogic;
using BookStoreAPI.Helpers.BaseBusinessLogic;
using BookStoreData.Data;
using BookStoreData.Models.Products.BookItems;
using BookStoreViewModels.ViewModels.Products.BookItems;

namespace BookStoreAPI.BusinessLogic.BookItemsLogic
{
    public class BookItemB : BaseBusinessLogic<BookItem, BookItemsPostForView>
    {
        protected override async Task DeactivateAllConnectedEntities(BookItem entity, BookStoreContext context)
        {
            await BookDiscountManager.DeactivateAllDiscounts(entity, context);
            await BookDiscountCodeManager.DeactivateAllDiscountCodes(entity, context);
        }
    }
}
