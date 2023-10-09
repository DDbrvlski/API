using BookStoreAPI.Models.Helpers;
using BookStoreAPI.Models.Orders;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using BookStoreAPI.Models.Supplies.Dictionaries;
using BookStoreAPI.Models.Transactions.Dictionaries;

namespace BookStoreAPI.Models.Supply
{
    public class Supply : BaseEntity
    {
        #region Properties
        public DateTime DeliveryDate { get; set; }
        #endregion
        #region Foreign Keys
        //Supplier
        [Required(ErrorMessage = "Dostawca jest wymagana.")]
        [Display(Name = "Dostawca")]
        public int? SupplierID { get; set; }

        [ForeignKey("SupplierID")]
        public virtual Supplier Supplier { get; set; }

        //DeliveryStatus
        [Required(ErrorMessage = "Status dostawy jest wymagany.")]
        [Display(Name = "Status dostawy")]
        public int? DeliveryStatusID { get; set; }

        [ForeignKey("DeliveryStatusID")]
        public virtual DeliveryStatus DeliveryStatus { get; set; }

        //PaymentMethod
        [Required(ErrorMessage = "Metoda płatności jest wymagana.")]
        [Display(Name = "Metoda płatności")]
        public int? PaymentMethodID { get; set; }

        [ForeignKey("PaymentMethodID")]
        public virtual PaymentMethod PaymentMethod { get; set; }
        #endregion
    }
}
