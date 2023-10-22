using BookStoreAPI.Models.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using BookStoreAPI.Models.Customers.AddressDictionaries;

namespace BookStoreAPI.Models.Customers
{
    public class Address : BaseEntity
    {
        #region Properties
        public string Street { get; set; }
        public string? StreetNumber { get; set; }
        public string? HouseNumber { get; set; }
        public string? Postcode { get; set; }
        #endregion
        #region Foreign Keys
        //City
        [Required(ErrorMessage = "Miasto jest wymagane.")]
        [Display(Name = "Miasto")]
        public int? CityID { get; set; }

        [ForeignKey("CityID")]
        public virtual City City { get; set; }

        //Country
        [Required(ErrorMessage = "Kraj jest wymagany.")]
        [Display(Name = "Kraj")]
        public int? CountryID { get; set; }

        [ForeignKey("CountryID")]
        public virtual Country Country { get; set; }
        #endregion
    }
}
