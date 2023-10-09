using BookStoreAPI.Models.Helpers;
using BookStoreAPI.Models.Products.Books.BookDictionaries;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPI.Models.Products.Books
{
    public class BookAuthor : BaseEntity
    {
        //Author
        [Required(ErrorMessage = "Autor jest wymagany.")]
        [Display(Name = "Autor")]
        public int? AuthorID { get; set; }

        [ForeignKey("AuthorID")]
        public virtual Author Author { get; set; }

        //Book
        [Required(ErrorMessage = "Książka jest wymagana.")]
        [Display(Name = "Książka")]
        public int? BookID { get; set; }

        [ForeignKey("BookID")]
        public virtual Book Book { get; set; }
    }
}
