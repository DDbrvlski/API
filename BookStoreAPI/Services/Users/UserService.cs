using BookStoreAPI.BusinessLogic.AccountLogic;
using BookStoreAPI.Interfaces.Services;
using BookStoreData.Data;
using BookStoreData.Models.Accounts;
using BookStoreData.Models.Customers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BookStoreAPI.Services.Users
{
    public class UserService
        (UserManager<User> userManager, 
        BookStoreContext context, 
        IHttpContextAccessor httpContextAccessor)
        : IUserService
    {
        public async Task<bool> IsEmailAlreadyRegistered(string email)
        {
            var user = await context.User.FirstOrDefaultAsync(x => x.Email == email && x.IsActive);
            return user != null;
        }

        public async Task<User> GetUserByToken()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(userId == null)
            {
                return null;
            }

            return await context.User.FirstOrDefaultAsync(x => x.IsActive && x.Id == userId);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await context.User.FirstOrDefaultAsync(x => x.IsActive && x.Email == email);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await context.User.FirstOrDefaultAsync(x => x.IsActive && x.UserName == username);
        }

    }
}
