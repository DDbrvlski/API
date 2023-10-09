using BookStoreAPI.Models.Helpers;

namespace BookStoreAPI.Models.Accounts
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
