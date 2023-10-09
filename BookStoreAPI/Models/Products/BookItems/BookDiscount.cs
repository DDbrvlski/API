using BookStoreAPI.Models.Accounts;
using BookStoreAPI.Models.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BookStoreAPI.Models.Products.BookItems
{
    public class BookDiscount : BaseEntity
    {
        //BookItem
        [Required(ErrorMessage = "Książka jest wymagana.")]
        [Display(Name = "Książka")]
        public int? BookItemID { get; set; }

        [ForeignKey("BookItemID")]
        public virtual BookItem BookItem { get; set; }

        //Discount
        [Required(ErrorMessage = "Przecena jest wymagana.")]
        [Display(Name = "Przecena")]
        public int? DiscountID { get; set; }

        [ForeignKey("DiscountID")]
        public virtual Discount Discount { get; set; }
    }
}
