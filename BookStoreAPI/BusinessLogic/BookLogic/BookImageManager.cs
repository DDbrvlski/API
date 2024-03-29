﻿using BookStoreAPI.Helpers;
using BookStoreData.Data;
using BookStoreData.Models.Media;
using BookStoreData.Models.Products.Books;
using BookStoreViewModels.ViewModels.Products.Books.Dictionaries;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.BusinessLogic.BookLogic
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

            if (imagesToDeactivate.Count() > 0)
            {
                await DatabaseOperationHandler.HandleDatabaseOperation(
                    async () => await DeactivateChosenImages(book, imagesToDeactivate, _context),
                    "deaktywacji"
                );
            }

            if (imagesToAdd.Count() > 0)
            {
                await DatabaseOperationHandler.HandleDatabaseOperation(
                    async () => await AddNewImages(book, imagesToAdd, _context),
                    "dodawania"
                );
            }
        }
        public static async Task AddNewImages(Book book, List<ImagesForView?> imagesToAdd, BookStoreContext _context)
        {
            if (imagesToAdd?.Count > 0)
            {
                int positionCounter = 1;
                var newImages = imagesToAdd
                    .Where(image => image != null)
                    .Select(image => new Images
                    {
                        Title = image.Title,
                        ImageURL = image.ImageURL,
                        Position = positionCounter++
                    }).ToList();

                _context.Images.AddRange(newImages);

                await DatabaseOperationHandler.TryToSaveChangesAsync(_context);

                var bookImages = newImages
                    .Select(image => new BookImages
                    {
                        ImageID = image.Id,
                        BookID = book.Id
                    }).ToList();

                _context.BookImages.AddRange(bookImages);

                await DatabaseOperationHandler.TryToSaveChangesAsync(_context);
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

            await DatabaseOperationHandler.TryToSaveChangesAsync(_context);
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

            await DatabaseOperationHandler.TryToSaveChangesAsync(_context);
        }

    }
}
