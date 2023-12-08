using BookStoreData.Models.Accounts;

namespace BookStoreAPI.Interfaces.Services
{
    public interface IUserService
    {
        Task<bool> IsEmailAlreadyRegistered(string email);
        Task<User> GetUserByToken();
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByUsername(string username);
    }
}
