using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Models.Helpers
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;
    }
}
