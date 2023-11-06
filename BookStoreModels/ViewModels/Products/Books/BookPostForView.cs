using BookStoreViewModels.ViewModels.Helpers;
using BookStoreViewModels.ViewModels.Products.Books.Dictionaries;
using System.ComponentModel.DataAnnotations;

namespace BookStoreViewModels.ViewModels.Products.Books
{
    public class BookPostForView : BasePostView
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
