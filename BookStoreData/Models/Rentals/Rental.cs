using BookStoreData.Models.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BookStoreData.Models.Products.BookItems;
using BookStoreData.Models.Accounts;
using BookStoreData.Models.Rentals.Dictionaries;
using System.Text.Json.Serialization;

namespace BookStoreData.Models.Rental
{
    public class Rental : BaseEntity
    {
        #region Properties
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        #endregion
        #region Foreign Keys
        //BookItem
        [Required(ErrorMessage = "Książka jest wymagana.")]
        [Display(Name = "Książka")]
        public int? BookItemID { get; set; }

        [ForeignKey("BookItemID")]
        [JsonIgnore]
        public virtual BookItem BookItem { get; set; }

        //User
        [Required(ErrorMessage = "Użytkownik jest wymagany.")]
        [Display(Name = "Użytkownik")]
        public string? UserID { get; set; }

        [ForeignKey("UserID")]
        [JsonIgnore]
        public virtual User User { get; set; }

        //RentalType
        [Required(ErrorMessage = "Typ wynajmu jest wymagany.")]
        [Display(Name = "Typ wynajmu")]
        public int? RentalTypeID { get; set; }

        [ForeignKey("RentalTypeID")]
        [JsonIgnore]
        public virtual RentalType RentalType { get; set; }

        //RentalStatus
        [Required(ErrorMessage = "Status wynajmu jest wymagany.")]
        [Display(Name = "Status wynajmu")]
        public int? RentalStatusID { get; set; }

        [ForeignKey("RentalStatusID")]
        [JsonIgnore]
        public virtual RentalStatus RentalStatus { get; set; }
        #endregion
    }
}
