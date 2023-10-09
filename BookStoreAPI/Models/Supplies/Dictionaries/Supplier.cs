using BookStoreAPI.Models.Customers;
using BookStoreAPI.Models.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BookStoreAPI.Models.Supplies.Dictionaries
{
    public class Supplier : DictionaryTable
    {
        #region Properties
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        #endregion
        #region Foreign Keys
        //Address
        [Required(ErrorMessage = "Adres jest wymagany.")]
        [Display(Name = "Adres")]
        public int? AddressID { get; set; }

        [ForeignKey("AddressID")]
        public virtual Address Address { get; set; }
        #endregion
    }
}
