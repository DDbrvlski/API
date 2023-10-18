using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Products.Books;
using BookStoreAPI.ViewModels.Products.Books;
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
            Book newBook = new Book();
            newBook.CopyProperties(book);
            try
            {
                _context.Book.Add(newBook);
                _context.SaveChanges();
                try
                {
                    List<int?> listOfBookAuthors = book.ListOfBookAuthors.Select(x => x.Id).ToList();
                    await AddNewBookAuthor(newBook, listOfBookAuthors, _context);

                    List<int?> listOfBookCategories = book.ListOfBookCategories.Select(x => x.Id).ToList();
                    await AddNewBookCategory(newBook, listOfBookCategories, _context);

                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new BadRequestObjectResult(ex.Message);
                }
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        public static async Task<IActionResult> ConvertBookPostForViewAndUpdate(Book oldEntity, BookPostForView updatedEntity, BookStoreContext _context)
        {
            try
            {
                oldEntity.CopyProperties(updatedEntity);

                List<int?> existingAuthorIds = new List<int?>();
                List<int?> existingCategoryIds = new List<int?>();

                var authorIds = updatedEntity.ListOfBookAuthors.Select(x => x.Id).ToList();
                if (authorIds?.Count() > 0)
                {
                    existingAuthorIds = await _context.BookAuthor.Where(x => x.BookID == oldEntity.Id).Select(x => x.AuthorID).ToListAsync();
                }
                
                var categoryIds = updatedEntity.ListOfBookCategories.Select(x => x.Id).ToList();
                if (categoryIds?.Count() > 0)
                {
                    existingCategoryIds = await _context.BookCategory.Where(x => x.BookID == oldEntity.Id).Select(x => x.CategoryID).ToListAsync();
                }

                try
                {
                    if (existingAuthorIds?.Count() > 0)
                    {
                        var bookAuthorsToDeactive = existingAuthorIds.Where(x => !authorIds.Contains(x)).ToList();
                        var bookAuthorsToAdd = authorIds.Where(x => !existingAuthorIds.Contains(x.Value)).ToList();
                        if (bookAuthorsToDeactive?.Count > 0)
                        {
                            await DeactivateBookAuthor(oldEntity, bookAuthorsToDeactive, _context);
                        }
                        if (bookAuthorsToAdd?.Count > 0)
                        {
                            await AddNewBookAuthor(oldEntity, bookAuthorsToAdd, _context);
                        }
                    }

                    if (existingCategoryIds?.Count() > 0)
                    {
                        var bookCategoriesToDeactive = existingCategoryIds.Where(x => !categoryIds.Contains(x)).ToList();
                        var bookCategoriesToAdd = categoryIds.Where(x => !existingCategoryIds.Contains(x.Value)).ToList();
                        if (bookCategoriesToDeactive?.Count > 0)
                        {
                            await DeactivateBookCategory(oldEntity, bookCategoriesToDeactive, _context);
                        }
                        if (bookCategoriesToAdd?.Count > 0)
                        {
                            await AddNewBookCategory(oldEntity, bookCategoriesToAdd, _context);
                        }
                    }

                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new BadRequestObjectResult(ex.Message);
                }
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
        public static async Task<IActionResult> DeactivateBook(Book book, BookStoreContext _context)
        {
            try
            {
                book.IsActive = false;
                var listOfBookAuthorsToDeactive = _context.BookAuthor.Where(x => x.BookID == book.Id).ToList();
                var listOfBookCategoriesToDeactive = _context.BookCategory.Where(x => x.BookID == book.Id).ToList();
                if (listOfBookAuthorsToDeactive?.Count > 0)
                {
                    foreach (var item in listOfBookAuthorsToDeactive)
                    {
                        item.IsActive = false;
                    }
                }
                if (listOfBookCategoriesToDeactive?.Count > 0)
                {
                    foreach (var item in listOfBookCategoriesToDeactive)
                    {
                        item.IsActive = false;
                    }
                }
                return new OkResult();
            }
            catch(Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        private static async Task AddNewBookAuthor(Book book, List<int?> listOfBookAuthors, BookStoreContext _context)
        {
            foreach (var item in listOfBookAuthors)
            {
                BookAuthor bookAuthor = new BookAuthor();
                bookAuthor.AuthorID = item;
                bookAuthor.BookID = book.Id;
                _context.BookAuthor.Add(bookAuthor);
            }
        }

        private static async Task AddNewBookCategory(Book book, List<int?> listOfBookCategories, BookStoreContext _context)
        {
            foreach (var item in listOfBookCategories)
            {
                BookCategory bookCategory = new BookCategory();
                bookCategory.CategoryID = item;
                bookCategory.BookID = book.Id;
                _context.BookCategory.Add(bookCategory);
            }
        }

        private static async Task DeactivateBookAuthor(Book book, List<int?> listOfBookAuthorsToDeactive, BookStoreContext _context)
        {
            foreach (var item in listOfBookAuthorsToDeactive)
            {
                var bookAuthor = _context.BookAuthor.Where(x => x.BookID == book.Id && x.AuthorID == item).First();
                if (bookAuthor != null)
                {
                    bookAuthor.IsActive = false;
                }
            }
        }

        private static async Task DeactivateBookCategory(Book book, List<int?> listOfBookCategoriesToDeactive, BookStoreContext _context)
        {
            foreach (var item in listOfBookCategoriesToDeactive)
            {
                var bookCategory = _context.BookCategory.Where(x => x.BookID == book.Id && x.CategoryID == item).First();
                if (bookCategory != null)
                {
                    bookCategory.IsActive = false;
                }
            }
        }
    }
}
