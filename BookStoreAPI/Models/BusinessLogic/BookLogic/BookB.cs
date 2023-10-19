using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Media;
using BookStoreAPI.Models.Products.Books;
using BookStoreAPI.Models.Products.Books.BookDictionaries;
using BookStoreAPI.ViewModels.Products.Books;
using BookStoreAPI.ViewModels.Products.Books.Dictionaries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace BookStoreAPI.Models.BusinessLogic.BookLogic
{
    public static class BookB
    {
        public static async Task<IActionResult> ConvertBookPostForViewAndSave(BookPostForView bookWithData, BookStoreContext _context)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    Book newBook = new Book();
                    newBook.CopyProperties(bookWithData);

                    _context.Book.Add(newBook);
                    await _context.SaveChangesAsync();

                    await ConvertListsToUpdate(newBook, bookWithData, _context);

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
                    await _context.SaveChangesAsync();

                    await ConvertListsToUpdate(oldEntity, updatedEntity, _context);

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
                    await _context.SaveChangesAsync();

                    await DeactivateAllConnectedEntities(book, _context);

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

        private static async Task ConvertListsToUpdate(Book bookToUpdate, BookPostForView bookWithData, BookStoreContext _context)
        {
            List<int?> authorIds = bookWithData.ListOfBookAuthors.Select(x => x.Id).ToList();
            List<int?> categoryIds = bookWithData.ListOfBookCategories.Select(x => x.Id).ToList();
            List<ImagesForView> images = bookWithData.ListOfBookImages.ToList();

            await UpdateAllConnectedEntitiesLists(bookToUpdate, authorIds, categoryIds, images, _context);
        }

        private static async Task UpdateAllConnectedEntitiesLists(Book book, List<int?> authorIds, List<int?> categoryIds, List<ImagesForView?> images, BookStoreContext _context)
        {
            await BookAuthorManager.UpdateAuthors(book, authorIds, _context);
            await BookCategoryManager.UpdateCategories(book, categoryIds, _context);
            await BookImageManager.UpdateImages(book, images, _context);
        }

        private static async Task DeactivateAllConnectedEntities(Book book, BookStoreContext _context)
        {
            await BookAuthorManager.DeactivateAllAuthors(book, _context);
            await BookCategoryManager.DeactivateAllCategories(book, _context);
            await BookImageManager.DeactivateAllImages(book, _context);
        }
    }
}
