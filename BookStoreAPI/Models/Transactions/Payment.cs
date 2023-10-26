using BookStoreAPI.Models.Helpers;
using BookStoreAPI.Models.Orders;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using BookStoreAPI.Models.Transactions.Dictionaries;
using System.Transactions;
using System.Text.Json.Serialization;

namespace BookStoreAPI.Models.Transactions
{
    public class Payment : BaseEntity
    {
        #region Properties
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        #endregion
        #region Foreign Keys
        //Order
        [Required(ErrorMessage = "Zamówienie jest wymagana.")]
        [Display(Name = "Zamówienie")]
        public int? OrderID { get; set; }

        [ForeignKey("OrderID")]
        [JsonIgnore]
        public virtual Order Order { get; set; }

        //PaymentMethod
        [Required(ErrorMessage = "Metoda płatności jest wymagana.")]
        [Display(Name = "Metoda płatności")]
        public int? PaymentMethodID { get; set; }

        [ForeignKey("PaymentMethodID")]
        [JsonIgnore]
        public virtual PaymentMethod PaymentMethod { get; set; }

        //TransactionStatus
        [Required(ErrorMessage = "Status transakcji jest wymagany.")]
        [Display(Name = "Status transakcji")]
        public int? TransactionStatusID { get; set; }

        [ForeignKey("TransactionStatusID")]
        [JsonIgnore]
        public virtual TransactionStatus TransactionStatus { get; set; }
        #endregion
    }
}
