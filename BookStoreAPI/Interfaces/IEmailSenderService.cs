using BookStoreData.Models.Accounts;

namespace BookStoreAPI.Interfaces
{
    public interface IEmailSenderService
    {
        Task ResetPasswordEmail(string token, User user);
        Task ConfirmEmailEmail(string token, User user);
    }
}
