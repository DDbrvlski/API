using BookStoreAPI.Models.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookStoreAPI.Models.Products.BookItems
{
    public class BookDiscountCode : BaseEntity
    {
        //BookItem
        [Required(ErrorMessage = "Książka jest wymagana.")]
        [Display(Name = "Książka")]
        public int? BookItemID { get; set; }

        [ForeignKey("BookItemID")]
        [JsonIgnore]
        public virtual BookItem BookItem { get; set; }

        //DiscountCode
        [Required(ErrorMessage = "Kod promocyjny jest wymagany.")]
        [Display(Name = "Kod promocyjny")]
        public int? DiscountCodeID { get; set; }

        [ForeignKey("DiscountCodeID")]
        [JsonIgnore]
        public virtual DiscountCode DiscountCode { get; set; }
    }
}
