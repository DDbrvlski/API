using BookStoreAPI.Interfaces.Services;
using BookStoreData.Data;
using BookStoreData.Models.Accounts;
using BookStoreData.Models.Customers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Services.Users
{
    public class UserService(UserManager<User> userManager, BookStoreContext context) : IUserService
    {
        public async Task<bool> IsEmailAlreadyRegistered(string email)
        {
            var user = await context.User.FirstOrDefaultAsync(x => x.Email == email && x.IsActive);
            return user != null;
        }

    }
}
