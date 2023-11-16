using BookStoreAPI.Interfaces;
using BookStoreData.Models.Accounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Routing;

namespace BookStoreAPI.Services.Email
{
    public class EmailSenderService
        (IEmailService emailService, 
        IUrlHelperFactory urlHelperFactory,
        IHttpContextAccessor httpContextAccessor)
        : IEmailSenderService
    {
        public async Task ResetPasswordEmail(string token, User user)
        {
            var urlHelper = GetUrlHelper();
            var resetLink = urlHelper.Action("ResetPassword", "Account", new { userId = user.Id, token }, "https", "localhost:7247");
            var emailBody = $"Aby zresetować hasło, kliknij <a href='{resetLink}'>tutaj</a>.";
            emailService.SendEmail(user.Email, "Zresetuj hasło", emailBody);
        }

        public async Task ConfirmEmailEmail(string token, User user)
        {
            var urlHelper = GetUrlHelper();
            var confirmationLink = urlHelper.Action("ConfirmEmail", "Account", new { userId = user.Id, token }, "https", "localhost:7247");
            var emailBody = $"Aby potwierdzić adres email, klinij <a href='{confirmationLink}'>tutaj</a>.";
            emailService.SendEmail(user.Email, "Potwierdź email", emailBody);
        }

        private IUrlHelper GetUrlHelper()
        {
            var actionContext = new ActionContext(httpContextAccessor.HttpContext, httpContextAccessor.HttpContext.GetRouteData(), new ActionDescriptor());

            return urlHelperFactory.GetUrlHelper(actionContext);
        }
    }
}
