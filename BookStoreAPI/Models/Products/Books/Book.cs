using BookStoreAPI.Models.Helpers;
using BookStoreAPI.Models.Products.BookItems;
using BookStoreAPI.Models.Products.Books.BookDictionaries;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPI.Models.Products.Books
{
    public class Book : BaseEntity
    {
        #region Properties
        [Required(ErrorMessage = "Tytuł jest wymagany.")]
        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(10)]
        public string ISBN10 { get; set; }

        [MaxLength(13)]
        public string ISBN13 { get; set; }

        [Required(ErrorMessage = "Data publikacji jest wymagana.")]
        public DateTime PublishingDate { get; set; }

        [Required(ErrorMessage = "Liczba stron jest wymagana.")]
        [Range(1, int.MaxValue, ErrorMessage = "Liczba stron musi wynosić więcej niż zero.")]
        public int Pages { get; set; }

        public string Description { get; set; }
        #endregion
        #region Foreign Keys
        //Language
        [Display(Name = "Język")]
        public int? LanguageID { get; set; }

        [ForeignKey("LanguageID")]
        public virtual Language Language { get; set; }

        //OriginalLanguage
        [Required(ErrorMessage = "Oryginalny język jest wymagany.")]
        [Display(Name = "Oryginalny język")]
        public int? OriginalLanguageID { get; set; }

        [ForeignKey("OriginalLanguageID")]
        public virtual Language OriginalLanguage { get; set; }

        //Translator
        [Required(ErrorMessage = "Tłumacz jest wymagany.")]
        [Display(Name = "Tłumacz")]
        public int? TranslatorID { get; set; }

        [ForeignKey("TranslatorID")]
        public virtual Translator Translator { get; set; }

        //Publisher
        [Required(ErrorMessage = "Wydawca jest wymagany.")]
        [Display(Name = "Wydawca")]
        public int? PublisherID { get; set; }

        [ForeignKey("PublisherID")]
        public virtual Publisher Publisher { get; set; }
        #endregion
        #region Navigation properties
        public List<BookItem> BookItems { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }
        public List<BookCategory> BookCategories { get; set; }
        public List<BookDiscount> BookDiscounts { get; set; }
        public List<BookImages> BookImages { get; set; }
        public List<BookReview> BookReviews { get; set; }
        #endregion
    }
}
