using BookStoreAPI.Helpers.BaseBusinessLogic;
using BookStoreData.Data;
using BookStoreData.Models.Products.Books;
using BookStoreViewModels.ViewModels.Products.Books;
using BookStoreViewModels.ViewModels.Products.Books.Dictionaries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BookStoreAPI.BusinessLogic.BookLogic
{
    public class BookB : BaseBusinessLogic<Book, BookPostForView>
    {
        protected override async Task ConvertListsToUpdate(Book entity, BookPostForView entityWithData, BookStoreContext context)
        {
            List<int?> authorIds = entityWithData.ListOfBookAuthors.Select(x => x.Id).ToList();
            List<int?> categoryIds = entityWithData.ListOfBookCategories.Select(x => x.Id).ToList();
            List<ImagesForView> images = entityWithData.ListOfBookImages.ToList();

            await UpdateAllConnectedEntitiesLists(entity, authorIds, categoryIds, images, context);
        }

        protected override async Task DeactivateAllConnectedEntities(Book entity, BookStoreContext context)
        {
            await BookAuthorManager.DeactivateAllAuthors(entity, context);
            await BookCategoryManager.DeactivateAllCategories(entity, context);
            await BookImageManager.DeactivateAllImages(entity, context);
        }

        private static async Task UpdateAllConnectedEntitiesLists(Book book, List<int?> authorIds, List<int?> categoryIds, List<ImagesForView?> images, BookStoreContext _context)
        {
            await BookAuthorManager.UpdateAuthors(book, authorIds, _context);
            await BookCategoryManager.UpdateCategories(book, categoryIds, _context);
            await BookImageManager.UpdateImages(book, images, _context);
        }

        public static async Task<ActionResult<IEnumerable<BookForView>>> GetAllBooks(BookStoreContext context)
        {
            return await context.Book
                .Include(x => x.Publisher)
                .Include(x => x.BookAuthors)
                    .ThenInclude(x => x.Author)
                .Include(x => x.BookCategories)
                    .ThenInclude(x => x.Category)
                .Where(x => x.IsActive == true)
                .Select(x => new BookForView
                {
                    Id = x.Id,
                    PublisherName = x.Publisher.Name,
                    Title = x.Title,
                    Authors = x.BookAuthors
                            .Where(y => y.IsActive == true)
                            .Select(y => new AuthorsForView
                            {
                                Id = y.Author.Id,
                                Name = y.Author.Name,
                                Surname = y.Author.Surname,
                            }).ToList()
                })
                .ToListAsync();
        }

        public static async Task<ActionResult<BookDetailsForView?>> GetBookById(BookStoreContext context, int id)
        {
            return await context.Book
                .Include(x => x.OriginalLanguage)
                .Include(x => x.Publisher)
                .Include(x => x.BookAuthors)
                    .ThenInclude(x => x.Author)
                .Include(x => x.BookCategories)
                    .ThenInclude(x => x.Category)
                .Include(x => x.BookImages)
                    .ThenInclude(x => x.Image)
                .Where(x => x.Id == id && x.IsActive)
                .Select(element => new BookDetailsForView
                {
                    Id = element.Id,
                    OriginalLanguageName = element.OriginalLanguage.Name,
                    PublisherName = element.Publisher.Name,
                    Description = element.Description,
                    OriginalLanguageID = element.OriginalLanguageID,
                    PublisherID = element.PublisherID,
                    Title = element.Title,
                    Categories = element.BookCategories
                            .Where(z => z.IsActive == true)
                            .Select(y => new CategoryForView
                            {
                                Id = y.Category.Id,
                                Name = y.Category.Name,
                            }).ToList(),
                    Authors = element.BookAuthors
                            .Where(z => z.IsActive == true)
                            .Select(y => new AuthorsForView
                            {
                                Id = y.Author.Id,
                                Name = y.Author.Name,
                                Surname = y.Author.Surname,
                            }).ToList(),
                    Images = element.BookImages
                            .Where(y => y.IsActive == true)
                            .Select(y => new ImagesForView
                            {
                                Id = y.Image.Id,
                                Title = y.Image.Title,
                                ImageURL = y.Image.ImageURL,
                            }).ToList(),
                })
                .FirstAsync();
        }

    }
}
