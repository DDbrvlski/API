using BookStoreData.Models.Helpers;

namespace BookStoreData.Models.Accounts
{
    public class LoginDetails : BaseEntity
    {
        public string Email { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
    }
}
