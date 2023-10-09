using BookStoreAPI.Models.Helpers;

namespace BookStoreAPI.Models.Accounts
{
    public class LoginDetails : BaseEntity
    {
        public string Email { get; set; } 
        public string Login { get; set; } 
        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }
    }
}
