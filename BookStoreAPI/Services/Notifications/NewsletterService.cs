using BookStoreAPI.Helpers;
using BookStoreAPI.Interfaces.Services;
using BookStoreData.Data;
using BookStoreData.Models.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Services.Notifications
{
    public class NewsletterService
        (BookStoreContext context,
        IUserService userService,
        ICustomerService customerService,
        IEmailService emailSender)
        : INewsletterService
    {
        public async Task<IActionResult> AddNewsletterUser(string email)
        {
            var isEmailExists = await userService.IsEmailAlreadyRegistered(email);

            if (isEmailExists)
            {
                await customerService.SetCustomerNewsletterSubscription(await customerService.GetCustomerByEmail(email));
            }

            if (await IsAlreadySubscribed(email))
            {
                return new BadRequestObjectResult("E-mail jest już zasubskrybowany do newslettera.");
            }

            return await AddToNewsletterSubscribers(email);
        }

        public async Task<IActionResult> CreateNewsletter(Newsletter newsletter)
        {
            context.Newsletter.Add(newsletter);
            return await DatabaseOperationHandler.TryToSaveChangesAsync(context);
        }

        public async Task<IActionResult> AddToNewsletterSubscribers(string email)
        {
            var newNewsletterUser = new NewsletterSubscribers()
            {
                Email = email,
            };

            context.NewsletterSubscribers.Add(newNewsletterUser);

            return await DatabaseOperationHandler.TryToSaveChangesAsync(context);
        }

        public async Task<IActionResult> RemoveFromNewsletterSubscribers(string email)
        {
            var subscriberToRemove = await context.NewsletterSubscribers.FirstOrDefaultAsync(x => x.Email == email && x.IsActive);
            if (subscriberToRemove != null)
            {
                subscriberToRemove.IsActive = false;
                return await DatabaseOperationHandler.TryToSaveChangesAsync(context);
            }

            return new BadRequestObjectResult("Podany adres e-mail nie jest zasubskrybowany do newslettera.");
        }

        public async Task SendNewsletterToSubscribers()
        {
            //var newslettersToSend = await context.Newsletter.Where(x => x.IsActive && x.PublicationDate.ToShortDateString == DateTime.Now.ToShortDateString).ToListAsync();

            //Wysyłanie ostatniego newslettera w ramach testu
            var newslettersToSend = await context.Newsletter.OrderByDescending(x => x.Id).FirstOrDefaultAsync(x => x.IsActive);
            var newsletterSubscribers = await context.NewsletterSubscribers.Where(x => x.IsActive).ToListAsync();

            if (newslettersToSend != null && newsletterSubscribers != null)
            {
                foreach (var subscriber in newsletterSubscribers)
                {
                    emailSender.SendEmail(subscriber.Email, newslettersToSend.Title, newslettersToSend.Content);
                }
            }
        }

        private async Task<bool> IsAlreadySubscribed(string email)
        {
            var subscriber = await context.NewsletterSubscribers.FirstOrDefaultAsync(x => x.Email == email && x.IsActive);

            if (subscriber != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
