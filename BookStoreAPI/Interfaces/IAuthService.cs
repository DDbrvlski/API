using BookStoreData.Models.Accounts;
using BookStoreViewModels.ViewModels.Accounts.Account;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Interfaces
{
    public interface IAuthService
    {
        Task<(int, string)> Registration(RegisterForView model, string role);
        Task<(int, string)> Login(LoginForView model);
        Task<(int, string)> CheckTokenValidity(string token);
        string DecodeToken(string token);
    }
}
