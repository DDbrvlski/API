using BookStoreAPI.Interfaces.Services;
using BookStoreData.Models.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Notifications
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsletterController(INewsletterService newsletterService) : ControllerBase
    {
        [HttpPost]
        [Route("Add-New-Subscriber")]
        public async Task<IActionResult> AddNewNewsletterUser(string email)
        {
            return await newsletterService.AddNewsletterUser(email);
        }

        [HttpPost]
        [Route("Create-New-Newsletter")]
        public async Task<IActionResult> CreateNewsletter(Newsletter newsletter)
        {
            return await newsletterService.CreateNewsletter(newsletter);
        }

        [HttpGet]
        [Route("SEND")]
        public async Task SendNewsletters()
        {
            await newsletterService.SendNewsletterToSubscribers();
        }
    }
}
