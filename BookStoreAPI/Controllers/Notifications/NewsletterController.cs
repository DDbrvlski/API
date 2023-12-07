using BookStoreAPI.Helpers.BaseController;
using BookStoreAPI.Interfaces.Services;
using BookStoreData.Data;
using BookStoreData.Models.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Notifications
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsletterController : CRUDController<Newsletter>
    {
        private readonly INewsletterService _newsletterService;
        public NewsletterController(BookStoreContext context, INewsletterService newsletterService) : base(context)
        {
            _newsletterService = newsletterService;
        }

        [HttpPost]
        [Route("Add-New-Subscriber")]
        public async Task<IActionResult> AddNewNewsletterUser(string email)
        {
            return await _newsletterService.AddNewsletterUser(email);
        }

        [HttpPost]
        [Route("Create-New-Newsletter")]
        public async Task<IActionResult> CreateNewsletter(Newsletter newsletter)
        {
            return await _newsletterService.CreateNewsletter(newsletter);
        }

        [HttpGet]
        [Route("SEND")]
        public async Task SendNewsletters()
        {
            await _newsletterService.SendNewsletterToSubscribers();
        }
    }
}
