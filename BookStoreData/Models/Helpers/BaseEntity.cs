using System.ComponentModel.DataAnnotations;

namespace BookStoreData.Models.Helpers
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
    }
}
