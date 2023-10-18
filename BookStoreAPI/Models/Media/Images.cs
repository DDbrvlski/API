using BookStoreAPI.Models.Helpers;

namespace BookStoreAPI.Models.Media
{
    public class Images : BaseEntity
    {
        public string? Title { get; set; }
        public string? ImageURL { get; set; }
    }
}
