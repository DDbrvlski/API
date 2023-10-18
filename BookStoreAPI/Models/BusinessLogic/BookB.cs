using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Media;
using BookStoreAPI.Models.Products.Books;
using BookStoreAPI.ViewModels.Products.Books;
using BookStoreAPI.ViewModels.Products.Books.Dictionaries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Principal;

namespace BookStoreAPI.Models.BusinessLogic
{
    public class BookB
    {
        public static async Task<IActionResult> ConvertBookPostForViewAndSave(BookPostForView book, BookStoreContext _context)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    Book newBook = new Book();
                    newBook.CopyProperties(book);

                    _context.Book.Add(newBook);
                    _context.SaveChanges();

                    List<int?> listOfBookAuthors = book.ListOfBookAuthors.Select(x => x.Id).ToList();
                    List<int?> listOfBookCategories = book.ListOfBookCategories.Select(x => x.Id).ToList();
                    List<ImagesForView?> listOfBookImages = book.ListOfBookImages.ToList();

                    await UpdateAuthorCategoryImageLists(newBook, listOfBookAuthors, listOfBookCategories, listOfBookImages, _context);

                    transaction.Commit();
                    return new OkResult();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new BadRequestObjectResult(ex.Message);
                }
            }
        }

        public static async Task<IActionResult> ConvertBookPostForViewAndUpdate(Book oldEntity, BookPostForView updatedEntity, BookStoreContext _context)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    oldEntity.CopyProperties(updatedEntity);

                    List<int?> authorIds = updatedEntity.ListOfBookAuthors.Select(x => x.Id).ToList();
                    List<int?> categoryIds = updatedEntity.ListOfBookCategories.Select(x => x.Id).ToList();
                    List<ImagesForView> images = updatedEntity.ListOfBookImages.ToList();

                    await UpdateAuthorCategoryImageLists(oldEntity, authorIds, categoryIds, images, _context);

                    transaction.Commit();
                    return new OkResult();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new BadRequestObjectResult(ex.Message);
                }
            }
        }
        public static async Task<IActionResult> DeactivateBook(Book book, BookStoreContext _context)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    book.IsActive = false;

                    await DeactivateBookAuthorsCategoriesImages(book, _context);

                    transaction.Commit();
                    return new OkResult();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new BadRequestObjectResult(ex.Message);
                }
            }
        }

        private static async Task UpdateAuthorCategoryImageLists(Book book, List<int?> authorIds, List<int?> categoryIds, List<ImagesForView?> images, BookStoreContext _context)
        {
            var existingAuthorIds = await _context.BookAuthor
                .Where(x => x.BookID == book.Id)
                .Select(x => x.AuthorID)
                .ToListAsync();

            var existingCategoryIds = await _context.BookCategory
                .Where(x => x.BookID == book.Id)
                .Select(x => x.CategoryID)
                .ToListAsync();

            var existingImageIds = await _context.BookCategory
                .Where(x => x.BookID == book.Id)
                .Select(x => x.CategoryID)
                .ToListAsync();

            var imageIds = images.Select(x => (int?)x.Id).ToList();

            var authorsToDeactivate = existingAuthorIds.Except(authorIds).ToList();
            var authorsToAdd = authorIds.Except(existingAuthorIds).ToList();

            var categoriesToDeactivate = existingCategoryIds.Except(categoryIds).ToList();
            var categoriesToAdd = categoryIds.Except(existingCategoryIds).ToList();

            var imagesToDeactivate = existingImageIds.Except(imageIds).ToList();
            var imagesToAdd = images.Where(x => !existingImageIds.Contains((int?)x.Id)).ToList();

            await DeactivateAuthorsCategoriesImages(book, authorsToDeactivate, categoriesToDeactivate, imageIds, _context);
            await AddNewAuthorsCategoriesImages(book, authorsToAdd, categoriesToAdd, imagesToAdd, _context);
        }

        private static async Task DeactivateAuthorsCategoriesImages(Book book, List<int?> authors, List<int?> categories, List<int?> images, BookStoreContext _context)
        {
            foreach (var authorId in authors)
            {
                var author = await _context.BookAuthor
                    .FirstOrDefaultAsync(x => x.BookID == book.Id && x.AuthorID == authorId);

                if (author != null)
                {
                    author.IsActive = false;
                }
            }

            foreach (var categoryId in categories)
            {
                var category = await _context.BookCategory
                    .FirstOrDefaultAsync(x => x.BookID == book.Id && x.CategoryID == categoryId);

                if (category != null)
                {
                    category.IsActive = false;
                }
            }

            foreach (var imageId in images)
            {
                var image = await _context.BookImages
                    .FirstOrDefaultAsync(x => x.BookID == book.Id && x.ImageID == imageId);

                if (image != null)
                {
                    image.IsActive = false;
                }
            }
        }

        private static async Task AddNewAuthorsCategoriesImages(Book book, List<int?> authors, List<int?> categories, List<ImagesForView> images, BookStoreContext _context)
        {
            foreach (var authorId in authors)
            {
                BookAuthor bookAuthor = new BookAuthor
                {
                    AuthorID = authorId,
                    BookID = book.Id
                };

                _context.BookAuthor.Add(bookAuthor);
            }

            foreach (var categoryId in categories)
            {
                BookCategory bookCategory = new BookCategory
                {
                    CategoryID = categoryId,
                    BookID = book.Id
                };

                _context.BookCategory.Add(bookCategory);
            }

            if (images?.Count > 0)
            {
                foreach (var image in images)
                {
                    Images newImage = new Images
                    {
                        Title = image.Title,
                        ImageURL = image.ImageURL,
                    };

                    _context.Images.Add(newImage);
                    _context.SaveChanges();

                    BookImages bookImage = new BookImages
                    {
                        ImageID = newImage.Id,
                        BookID = book.Id
                    };
                }
            }
        }

        private static async Task DeactivateBookAuthorsCategoriesImages(Book book, BookStoreContext _context)
        {
            var authors = await _context.BookAuthor
                .Where(x => x.BookID == book.Id)
                .ToListAsync();

            var categories = await _context.BookCategory
                .Where(x => x.BookID == book.Id)
                .ToListAsync();

            var images = await _context.BookImages
                .Where(x => x.BookID == book.Id)
                .ToListAsync();

            foreach (var author in authors)
            {
                author.IsActive = false;
            }

            foreach (var category in categories)
            {
                category.IsActive = false;
            }

            foreach (var image in images)
            {
                image.IsActive = false;
            }
        }
    }
}
