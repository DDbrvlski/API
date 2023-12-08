using BookStoreData.Data;
using BookStoreData.Models.Wishlist;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.BusinessLogic.WishlistLogic
{
    public class WishlistItemManager
    {
        public static async Task DeactivateAllWishlistItems(Wishlist wishlist, BookStoreContext context)
        {
            var wishlistItemsToDeactivate = await context.WishlistItems.Where(x => x.IsActive && x.WishlistID == wishlist.Id).ToListAsync();

            foreach (var item in wishlistItemsToDeactivate)
            {
                item.IsActive = false;
            }
        }
    }
}
