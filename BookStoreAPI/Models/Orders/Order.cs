using BookStoreAPI.Models.Helpers;
using BookStoreAPI.Models.Products.Books.BookDictionaries;
using BookStoreAPI.Models.Products.Books;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using BookStoreAPI.Models.Orders.Dictionaries;
using System.Text.Json.Serialization;

namespace BookStoreAPI.Models.Orders
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }

        //OrderStatus
        [Required(ErrorMessage = "Status zamówienia jest wymagany.")]
        [Display(Name = "Status zamówienia")]
        public int? OrderStatusID { get; set; }

        [ForeignKey("OrderStatusID")]
        [JsonIgnore]
        public virtual OrderStatus OrderStatus { get; set; }

        //DeliveryMethod
        [Required(ErrorMessage = "Sposób dostawy jest wymagany.")]
        [Display(Name = "Sposób dostawy")]
        public int? DeliveryMethodID { get; set; }

        [ForeignKey("DeliveryMethodID")]
        [JsonIgnore]
        public virtual DeliveryMethod DeliveryMethod { get; set; }
    }
}
