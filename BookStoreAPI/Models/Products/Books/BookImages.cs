using BookStoreAPI.Models.Helpers;
using BookStoreAPI.Models.Products.Books.BookDictionaries;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using BookStoreAPI.Models.Media;

namespace BookStoreAPI.Models.Products.Books
{
    public class BookImages : BaseEntity
    {
        //Image
        [Required(ErrorMessage = "Zdjęcie jest wymagane.")]
        [Display(Name = "Zdjęcie")]
        public int? ImageID { get; set; }

        [ForeignKey("ImageID")]
        public virtual Images Image { get; set; }

        //Book
        [Required(ErrorMessage = "Książka jest wymagana.")]
        [Display(Name = "Książka")]
        public int? BookID { get; set; }

        [ForeignKey("BookID")]
        public virtual Book Book { get; set; }
    }
}
