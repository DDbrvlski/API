using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using BookStoreAPI.Models.Delivery.Dictionaries;
using BookStoreAPI.Models.Customers;
using BookStoreAPI.Models.Orders;
using System.Text.Json.Serialization;

namespace BookStoreAPI.Models.Delivery
{
    public class Shipping : BaseEntity
    {
        #region Properties
        public DateTime? ShippingDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        #endregion
        #region Foreign Keys

        //Address
        [Required(ErrorMessage = "Adres jest wymagany.")]
        [Display(Name = "Adres")]
        public int? AddressID { get; set; }

        [ForeignKey("AddressID")]
        [JsonIgnore]
        public virtual Address Address { get; set; }

        //ShippingStatus
        [Required(ErrorMessage = "Status wysyłki jest wymagany.")]
        [Display(Name = "Status wysyłki")]
        public int? ShippingStatusID { get; set; }

        [ForeignKey("ShippingStatusID")]
        [JsonIgnore]
        public virtual ShippingStatus ShippingStatus { get; set; }
        #endregion
    }
}
