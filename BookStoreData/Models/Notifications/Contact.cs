using BookStoreData.Models.Helpers;

namespace BookStoreData.Models.Notifications
{
    public class Contact : BaseEntity
    {
        public string? ClientName { get; set; }
        public string? Email { get; set; }
        public string? Content { get; set; }
    }
}
