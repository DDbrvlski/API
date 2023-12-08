using BookStoreAPI.BusinessLogic.WishlistLogic;
using BookStoreAPI.Helpers;
using BookStoreAPI.Interfaces.Services;
using BookStoreData.Data;
using BookStoreData.Models.Accounts;
using BookStoreData.Models.Wishlist;
using BookStoreViewModels.ViewModels.Wishlists;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BookStoreAPI.Controllers.Wishlist
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController(BookStoreContext context, IUserService userService) : ControllerBase
    {
        [HttpGet]
        [Route("{publicIdentifier}")]
        public async Task<ActionResult<WishlistForView>> GetUserWishlist(Guid publicIdentifier)
        {
            return await WishlistB.GetUserWishlistAsync(publicIdentifier, context);
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<Guid>> GetUserWishlist()
        {
            return await WishlistB.GetUserWishlistAsync(context, userService);
        }

        [HttpPost]
        [Route("Edit-Wishlist-Item")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> EditUserWishlistItem(int bookItemId, bool isWishlisted)
        {
            return await WishlistB.EditUserWishlistItemAsync(bookItemId, isWishlisted, userService, context);
        }
    }
}
