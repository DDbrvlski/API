using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using BookStoreAPI.Models.Accounts.Dictionaries;
using BookStoreAPI.Models.Customers;
using BookStoreAPI.Models.Media;

namespace BookStoreAPI.Models.Accounts
{
    public class User : BaseEntity
    {
        #region Foreign Keys
        //Permission
        [Required(ErrorMessage = "Dostęp jest wymagany.")]
        [Display(Name = "Dostęp")]
        public int? PermissionID { get; set; }

        [ForeignKey("PermissionID")]
        public virtual Permission Permission { get; set; }

        //LoginDetails
        [Required(ErrorMessage = "Dane logowania są wymagane.")]
        [Display(Name = "Dane logowania")]
        public int? LoginDetailsID { get; set; }

        [ForeignKey("LoginDetailsID")]
        public virtual LoginDetails LoginDetails { get; set; }

        //AccountStatus
        [Required(ErrorMessage = "Status konta jest wymagany.")]
        [Display(Name = "Status konta")]
        public int? AccountStatusID { get; set; }

        [ForeignKey("AccountStatusID")]
        public virtual AccountStatus AccountStatus { get; set; }

        //Customer
        [Required(ErrorMessage = "Dane klienta są wymagane.")]
        [Display(Name = "Dane klienta")]
        public int? CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }

        //Image
        [Display(Name = "Zdjęcie")]
        public int? ImageID { get; set; }

        [ForeignKey("ImageID")]
        public virtual Images Image { get; set; }
        #endregion
    }
}
