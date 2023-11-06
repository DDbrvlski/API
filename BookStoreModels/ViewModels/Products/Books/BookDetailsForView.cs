using BookStoreViewModels.ViewModels.Helpers;
using BookStoreViewModels.ViewModels.Products.Books.Dictionaries;
using System.ComponentModel.DataAnnotations;

namespace BookStoreViewModels.ViewModels.Products.Books
{
    public class BookDetailsForView : BaseView
    {
        [Required(ErrorMessage = "Tytuł jest wymagany.")]
        [MaxLength(255)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int? OriginalLanguageID { get; set; }
        public string OriginalLanguageName { get; set; }
        public int? PublisherID { get; set; }
        public string PublisherName { get; set; }
        public List<CategoryForView> Categories { get; set; } = new List<CategoryForView>();
        public List<AuthorsForView> Authors { get; set; } = new List<AuthorsForView>();
        public List<ImagesForView> Images { get; set; } = new List<ImagesForView>();
    }
}
