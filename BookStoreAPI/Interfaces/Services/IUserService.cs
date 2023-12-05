namespace BookStoreAPI.Interfaces.Services
{
    public interface IUserService
    {
        Task<bool> IsEmailAlreadyRegistered(string email);
    }
}
