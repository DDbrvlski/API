using BookStoreAPI.Data;
using BookStoreAPI.Models.BusinessLogic.BookLogic;
using BookStoreAPI.Models.Products.Books;
using BookStoreAPI.ViewModels.Products.Books.Dictionaries;
using BookStoreAPI.ViewModels.Products.Books;
using Microsoft.AspNetCore.Mvc;
using BookStoreAPI.Helpers;
using BookStoreAPI.ViewModels.Products.BookItems;
using BookStoreAPI.Models.Products.BookItems;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Models.BusinessLogic.BookItemsLogic
{
    public class BookItemB
    {
        public static async Task<IActionResult> ConvertBookItemPostForViewAndSave(BookItemsPostForView bookItemWithData, BookStoreContext _context)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await AddNewBookItem(bookItemWithData, _context);

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

        public static async Task<IActionResult> ConvertBookItemPostForViewAndUpdate(BookItem oldEntity, BookItemsPostForView updatedEntity, BookStoreContext _context)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await UpdateBookItem(oldEntity, updatedEntity, _context);

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

        public static async Task<IActionResult> DeactivateBookItem(BookItem bookItem, BookStoreContext _context)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await DeactivateBookItemCustom(bookItem, _context);

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

        private static async Task DeactivateBookItemCustom(BookItem bookItem, BookStoreContext _context)
        {
            bookItem.IsActive = false;
            await _context.SaveChangesAsync();
        }

        private static async Task AddNewBookItem(BookItemsPostForView bookItemWithData, BookStoreContext _context)
        {
            BookItem newBook = new BookItem();
            newBook.CopyProperties(bookItemWithData);

            _context.BookItem.Add(newBook);
            await _context.SaveChangesAsync();
        }

        private static async Task UpdateBookItem(BookItem oldEntity, BookItemsPostForView updatedEntity, BookStoreContext _context)
        {
            oldEntity.CopyProperties(updatedEntity);
            await _context.SaveChangesAsync();
        }
    }
}
