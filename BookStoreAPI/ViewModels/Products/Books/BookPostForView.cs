using BookStoreAPI.Models.Products.Books.BookDictionaries;
using BookStoreAPI.ViewModels.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BookStoreAPI.ViewModels.Products.Books.Dictionaries;

namespace BookStoreAPI.ViewModels.Products.Books
{
    public class BookPostForView : BaseView
    {
        [Required(ErrorMessage = "Tytuł jest wymagany.")]
        [MaxLength(255)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Oryginalny język jest wymagany.")]
        [Display(Name = "Oryginalny język")]
        public int? OriginalLanguageID { get; set; }

        [Required(ErrorMessage = "Wydawca jest wymagany.")]
        [Display(Name = "Wydawca")]
        public int? PublisherID { get; set; }

        public List<ListOfIds>? ListOfBookAuthors { get; set; }
        public List<ListOfIds>? ListOfBookCategories { get; set; }
        public List<ImagesForView>? ListOfBookImages { get; set; }
    }
}
