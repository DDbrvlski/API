using BookStoreData.Models.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BookStoreData.Models.Accounts;
using BookStoreData.Models.Products.Books.BookDictionaries;
using System.Text.Json.Serialization;

namespace BookStoreData.Models.Products.Books
{
    public class BookReview : BaseEntity
    {
        public string? Content { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        //User
        [Required(ErrorMessage = "Użytkownik jest wymagany.")]
        [Display(Name = "Użytkownik")]
        public int? UserID { get; set; }

        [ForeignKey("UserID")]
        [JsonIgnore]
        public virtual User User { get; set; }

        //Book
        [Required(ErrorMessage = "Książka jest wymagana.")]
        [Display(Name = "Książka")]
        public int? BookID { get; set; }

        [ForeignKey("BookID")]
        [JsonIgnore]
        public virtual Book Book { get; set; }

        //Score
        [Required(ErrorMessage = "Ocena jest wymagana.")]
        [Display(Name = "Ocena")]
        public int? ScoreID { get; set; }

        [ForeignKey("ScoreID")]
        [JsonIgnore]
        public virtual Score Score { get; set; }
    }
}
