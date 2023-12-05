using BookStoreData.Models.Accounts;

namespace BookStoreAPI.Interfaces.Services
{
    public interface IEmailSenderService
    {
        Task ResetPasswordEmail(string token, User user);
        Task ConfirmEmailEmail(string token, User user);
        Task ConfirmationOfContact(string userName, string userEmail);
    }
}
