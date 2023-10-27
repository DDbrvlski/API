using BookStoreAPI.Models.Customers;
using BookStoreAPI.Models.Delivery;
using BookStoreAPI.Models.Helpers;
using BookStoreAPI.Models.Orders.Dictionaries;
using BookStoreAPI.Models.Transactions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        //Payment
        [Required(ErrorMessage = "Sposób dostawy jest wymagany.")]
        [Display(Name = "Sposób dostawy")]
        public int? PaymentID { get; set; }

        [ForeignKey("PaymentID")]
        [JsonIgnore]
        public virtual Payment Payment { get; set; }

        //Shipping
        [Required(ErrorMessage = "Sposób dostawy jest wymagany.")]
        [Display(Name = "Sposób dostawy")]
        public int? ShippingID { get; set; }

        [ForeignKey("ShippingID")]
        [JsonIgnore]
        public virtual Shipping Shipping { get; set; }

        //Customer
        [Required(ErrorMessage = "Sposób dostawy jest wymagany.")]
        [Display(Name = "Sposób dostawy")]
        public int? CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        [JsonIgnore]
        public virtual Customer Customer { get; set; }


        [JsonIgnore]
        public List<OrderItems>? OrderItems { get; set; }
    }
}
