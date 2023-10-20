using BookStoreAPI.Data;
using BookStoreAPI.Models.Media;
using BookStoreAPI.Models.Products.Books;
using BookStoreAPI.ViewModels.Products.Books.Dictionaries;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Models.BusinessLogic.BookLogic
{
    public class BookImageManager
    {
        public static async Task UpdateImages(Book book, List<ImagesForView?> images, BookStoreContext _context)
        {
            var existingImageIds = await _context.BookImages
                .Where(x => x.BookID == book.Id && x.IsActive == true)
                .Select(x => x.ImageID)
                .ToListAsync();

            var imageIds = images.Select(x => (int?)x.Id).ToList();

            var imagesToDeactivate = existingImageIds.Except(imageIds).ToList();
            var imagesToAdd = images.Where(x => x != null && !existingImageIds.Contains(x.Id)).ToList();

            await DeactivateChosenImages(book, imagesToDeactivate, _context);
            await AddNewImages(book, imagesToAdd, _context);
        }
        public static async Task AddNewImages(Book book, List<ImagesForView?> imagesToAdd, BookStoreContext _context)
        {
            if (imagesToAdd?.Count > 0)
            {
                var newImages = imagesToAdd
                    .Where(image => image != null)
                    .Select(image => new Images
                    {
                        Title = image.Title,
                        ImageURL = image.ImageURL
                    }).ToList();

                _context.Images.AddRange(newImages);
                await _context.SaveChangesAsync();

                var bookImages = newImages
                    .Select(image => new BookImages
                    {
                        ImageID = image.Id,
                        BookID = book.Id
                    }).ToList();

                _context.BookImages.AddRange(bookImages);
                await _context.SaveChangesAsync();
            }
        }
        public static async Task DeactivateAllImages(Book book, BookStoreContext _context)
        {
            var images = await _context.BookImages
                .Where(x => x.BookID == book.Id && x.IsActive == true)
                .ToListAsync();

            foreach (var image in images)
            {
                image.IsActive = false;
            }

            await _context.SaveChangesAsync();
        }
        public static async Task DeactivateChosenImages(Book book, List<int?> imageIdsToDeactivate, BookStoreContext _context)
        {
            var bookImagesToDeactivate = await _context.BookImages
                .Where(x => x.BookID == book.Id && imageIdsToDeactivate.Contains(x.ImageID) && x.IsActive == true)
                .ToListAsync();

            foreach (var bookImage in bookImagesToDeactivate)
            {
                bookImage.IsActive = false;
            }

            var imagesToDeactivate = await _context.Images
                .Where(x => imageIdsToDeactivate.Contains(x.Id) && x.IsActive == true)
                .ToListAsync();

            foreach (var image in imagesToDeactivate)
            {
                image.IsActive = false;
            }

            await _context.SaveChangesAsync();
        }

    }
}
