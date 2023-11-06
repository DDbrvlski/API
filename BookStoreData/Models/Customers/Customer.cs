using BookStoreData.Models.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BookStoreData.Models.Customers.CustomerDictionaries;
using System.Text.Json.Serialization;

namespace BookStoreData.Models.Customers
{
    public class Customer : BaseEntity
    {
        #region Properties
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsSubscribed { get; set; }
        #endregion
        #region Foreign Keys
        //Gender
        [Required(ErrorMessage = "Płeć jest wymagana.")]
        [Display(Name = "Płeć")]
        public int? GenderID { get; set; }

        [ForeignKey("GenderID")]
        public virtual Gender Gender { get; set; }
        #endregion
        [JsonIgnore]
        public List<CustomerAddress>? CustomerAddresses { get; set; }
    }
}
