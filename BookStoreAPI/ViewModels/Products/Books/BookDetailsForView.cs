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

        public string Description { get; set; }

        public string OriginalLanguageName { get; set; }
        public string PublisherName { get; set; }
        public List<CategoryForView> Categories { get; set; } = new List<CategoryForView>();
        public List<AuthorsForView> Authors { get; set; } = new List<AuthorsForView>();
        public List<ImagesForView> Images { get; set; } = new List<ImagesForView>();
    }
}
