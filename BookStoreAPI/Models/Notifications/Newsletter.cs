using BookStoreAPI.Models.Helpers;

namespace BookStoreAPI.Models.Notifications
{
    public class Newsletter : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublicationDate { get; set; }
        public bool IsSent { get; set; }
    }
}
