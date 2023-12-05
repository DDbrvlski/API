using BookStoreData.Models.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Interfaces.Services
{
    public interface INewsletterService
    {
        Task<IActionResult> AddNewsletterUser(string email);
        Task<IActionResult> AddToNewsletterSubscribers(string email);
        Task<IActionResult> RemoveFromNewsletterSubscribers(string email);
        Task SendNewsletterToSubscribers();
        Task<IActionResult> CreateNewsletter(Newsletter newsletter);
    }
}
