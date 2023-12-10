using BookStoreAPI.Helpers;
using BookStoreAPI.Interfaces.Services;
using BookStoreAPI.Services.Users;
using BookStoreData.Data;
using BookStoreData.Models.Accounts;
using BookStoreData.Models.Customers;
using BookStoreData.Models.Wishlist;
using BookStoreViewModels.ViewModels.Products.Books.Dictionaries;
using BookStoreViewModels.ViewModels.Wishlists;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BookStoreAPI.BusinessLogic.WishlistLogic
{
    public class WishlistB
    {
        public static async Task<ActionResult<WishlistForView>> GetUserWishlistAsync(Guid publicIdentifier, BookStoreContext context)
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
                            FormId = y.BookItem.FormID,
                            FileFormatName = y.BookItem.FileFormat.Name,
                            ImageURL = y.BookItem.Book.BookImages.Where(z => z.Image.Position == 1).FirstOrDefault().Image.ImageURL,
                            PriceBrutto = y.BookItem.NettoPrice * (1 + ((decimal)y.BookItem.VAT / 100)),
                            authors = y.BookItem.Book.BookAuthors.Select(y => new AuthorsForView
                            {
                                Id = (int)y.AuthorID,
                                Name = y.Author.Name,
                                Surname = y.Author.Surname,
                            }).ToList(),
                        }).ToList(),
                    FullPrice = x.WishlistItems
                        .Where(y => y.WishlistID == x.Id && y.IsActive)
                        .Sum(y => y.BookItem.NettoPrice * (1 + ((decimal)y.BookItem.VAT / 100)))
                }).FirstAsync();
        }
        public static async Task<ActionResult<Guid>> GetUserWishlistAsync(BookStoreContext context, IUserService userService)
        {
            var user = await userService.GetUserByToken();

            if (user == null)
            {
                return new NotFoundObjectResult("Nie można znaleźć użytkownika.");
            }

            return context.Wishlist.FirstAsync(x => x.CustomerID == user.CustomerID && x.IsActive).Result.PublicIdentifier;
        }
        public static async Task<IActionResult> EditUserWishlistItemAsync(int bookItemId, bool isWishlisted, IUserService userService, BookStoreContext context)
        {
            var user = await userService.GetUserByToken();

            if (user == null)
            {
                return new NotFoundObjectResult("Nie można znaleźć użytkownika.");
            }

            var userWishlistId = await context.Wishlist.FirstAsync(x => x.CustomerID == user.CustomerID && x.IsActive);

            if (!isWishlisted)
            {
                WishlistItems wishlistItem = new()
                {
                    BookItemID = bookItemId,
                    WishlistID = userWishlistId.Id,
                };
                context.WishlistItems.Add(wishlistItem);
            }
            else
            {
                var userWishlistItem = await context.WishlistItems.FirstAsync(x => x.WishlistID == userWishlistId.Id && x.IsActive && x.BookItemID == bookItemId);
                if (userWishlistItem != null)
                {
                    userWishlistItem.IsActive = false;
                }
            }

            return await DatabaseOperationHandler.TryToSaveChangesAsync(context);
        }
        public static async Task<IActionResult> DeactivateWishlistAsync(Customer customer, BookStoreContext context)
        {
            var wishlistToDeactivate = await context.Wishlist.FirstAsync(x => x.IsActive && x.CustomerID == customer.Id);
            wishlistToDeactivate.IsActive = false;

            await WishlistItemManager.DeactivateAllWishlistItems(wishlistToDeactivate, context);

            return await DatabaseOperationHandler.TryToSaveChangesAsync(context);
        }
    }
}
