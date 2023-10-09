using BookStoreAPI.Models.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using BookStoreAPI.Models.Products.Books;
using BookStoreAPI.Models.Products.BookItems.BookItemDictionaries;
using Microsoft.Data.SqlClient.Server;

namespace BookStoreAPI.Models.Products.BookItems
{
    public class BookItem : BaseEntity
    {
        #region Properties
        public float VAT { get; set; }
        public decimal NettoPrice { get; set; }
        #endregion
        #region Foreign Keys
        //Book
        [Required(ErrorMessage = "Książka jest wymagana.")]
        [Display(Name = "Książka")]
        public int? BookID { get; set; }

        [ForeignKey("BookID")]
        public virtual Book Book { get; set; }

        //Edition
        [Required(ErrorMessage = "Edycja jest wymagana.")]
        [Display(Name = "Edycja")]
        public int? EditionID { get; set; }

        [ForeignKey("EditionID")]
        public virtual Edition Edition { get; set; }

        //FileFormat
        [Required(ErrorMessage = "Format pliku jest wymagany.")]
        [Display(Name = "Format pliku")]
        public int? FileFormatID { get; set; }

        [ForeignKey("FileFormatID")]
        public virtual FileFormat FileFormat { get; set; }

        //Form
        [Required(ErrorMessage = "Forma jest wymagana.")]
        [Display(Name = "Forma")]
        public int? FormID { get; set; }

        [ForeignKey("FormID")]
        public virtual Form Form { get; set; }

        //Availability
        [Required(ErrorMessage = "Dostępność jest wymagana.")]
        [Display(Name = "Dostępność")]
        public int? AvailabilityID { get; set; }

        [ForeignKey("AvailabilityID")]
        public virtual Availability Availability { get; set; }
        #endregion
    }
}
