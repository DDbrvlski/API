using BookStoreAPI.Helpers;
using BookStoreData.Data;
using BookStoreData.Models.Accounts;
using BookStoreData.Models.Wishlist;
using BookStoreViewModels.ViewModels.Wishlists;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BookStoreAPI.Controllers.Wishlist
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController(BookStoreContext context, UserManager<User> userManager) : ControllerBase
    {
        [HttpGet]
        [Route("{publicIdentifier}")]
        public async Task<ActionResult<WishlistForView>> GetUserWishlist(Guid publicIdentifier)
        {
            return await context.Wishlist
                .Include(x => x.WishlistItems)
                    .ThenInclude(x => x.BookItem)
                    .ThenInclude(x => x.Book)
                .Include(x => x.WishlistItems)
                    .ThenInclude(x => x.BookItem)
                    .ThenInclude(x => x.Form)
                .Include(x => x.WishlistItems)
                    .ThenInclude(x => x.BookItem)
                    .ThenInclude(x => x.Edition)
                .Include(x => x.WishlistItems)
                    .ThenInclude(x => x.BookItem)
                    .ThenInclude(x => x.Book)
                    .ThenInclude(x => x.BookImages)
                    .ThenInclude(x => x.Image)
                .Where(x => x.PublicIdentifier == publicIdentifier && x.IsActive)
                .Select(x => new WishlistForView()
                {
                    Id = x.Id,
                    Items = x.WishlistItems
                        .Where(y => y.WishlistID == x.Id && y.IsActive)
                        .Select(y => new WishlistItemForView()
                        {
                            Id = y.Id,
                            BookTitle = y.BookItem.Book.Title,
                            EditionName = y.BookItem.Edition.Name,
                            FormName = y.BookItem.Form.Name,
                            ImageURL = y.BookItem.Book.BookImages.Where(z => z.Image.Position == 1).FirstOrDefault().Image.ImageURL,
                            PriceBrutto = y.BookItem.NettoPrice * (1 + ((decimal)y.BookItem.VAT / 100))
                        }).ToList(),
                    FullPrice = x.WishlistItems
                        .Where(y => y.WishlistID == x.Id && y.IsActive)
                        .Sum(y => y.BookItem.NettoPrice * (1 + ((decimal)y.BookItem.VAT / 100)))
                }).FirstOrDefaultAsync();
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<Guid>> GetUserWishlist()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest("Nie można znaleźć identyfikatora użytkownika.");
            }

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("Nie można znaleźć użytkownika o podanym identyfikatorze.");
            }

            return context.Wishlist.FirstAsync(x => x.CustomerID == user.CustomerID && x.IsActive).Result.PublicIdentifier;
        }

        [HttpPost]
        [Route("Edit-Wishlist-Item")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> EditUserWishlistItem(int bookItemId, bool isWishlisted)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest("Nie można znaleźć identyfikatora użytkownika.");
            }

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("Nie można znaleźć użytkownika o podanym identyfikatorze.");
            }

            var userWishlistId = context.Wishlist.FirstAsync(x => x.CustomerID == user.CustomerID && x.IsActive).Result.Id;

            if (!isWishlisted)
            {
                WishlistItems wishlistItem = new()
                {
                    BookItemID = bookItemId,
                    WishlistID = userWishlistId,
                };
                context.WishlistItems.Add(wishlistItem);
            }
            else
            {
                var userWishlistItem = await context.WishlistItems.FirstAsync(x => x.WishlistID == userWishlistId && x.IsActive && x.BookItemID == bookItemId);
                if (userWishlistItem != null)
                {
                    userWishlistItem.IsActive = false;
                }
            }

            return await DatabaseOperationHandler.TryToSaveChangesAsync(context);
        }
    }
}
