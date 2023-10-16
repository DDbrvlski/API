using BookStoreAPI.Models.Products.Books;
using BookStoreAPI.ViewModels.Helpers;
using BookStoreAPI.ViewModels.Products.Books.Dictionaries;
using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.ViewModels.Products.Books
{
    public class BookDetailsForView : BaseView
    {
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

        public string LanguageName { get; set; }
        public string TranslatorName { get; set; }
        public string PublisherName { get; set; }
        public List<CategoryForView> Categories { get; set; } = new List<CategoryForView>();
        public List<AuthorsForView> Authors { get; set; } = new List<AuthorsForView>();
    }
}
