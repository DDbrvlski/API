using BookStoreAPI.Models.Helpers;
using BookStoreAPI.Models.Products.Books;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BookStoreAPI.Models.Products.BookItems
{
    public class StockAmount : BaseEntity
    {
        public int Amount { get; set; }

        //BookItem
        [Required(ErrorMessage = "Książka jest wymagana.")]
        [Display(Name = "Książka")]
        public int? BookItemID { get; set; }

        [ForeignKey("BookItemID")]
        public virtual BookItem BookItem { get; set; }
    }
}
