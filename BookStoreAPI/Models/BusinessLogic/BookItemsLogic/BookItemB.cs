using BookStoreAPI.Data;
using BookStoreAPI.Helpers.BaseBusinessLogic;
using BookStoreAPI.Models.BusinessLogic.DiscountCodeCodeLogic;
using BookStoreAPI.Models.BusinessLogic.DiscountLogic;
using BookStoreAPI.Models.Products.BookItems;
using BookStoreAPI.ViewModels.Products.BookItems;

namespace BookStoreAPI.Models.BusinessLogic.BookItemsLogic
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
