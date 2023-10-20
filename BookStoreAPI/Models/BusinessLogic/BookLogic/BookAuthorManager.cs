using BookStoreAPI.Data;
using BookStoreAPI.Models.Products.Books;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Models.BusinessLogic.BookLogic
{
    public class BookAuthorManager
    {
        public static async Task UpdateAuthors(Book book, List<int?> authorIds, BookStoreContext _context)
        {
            var existingAuthorIds = await _context.BookAuthor
                .Where(x => x.BookID == book.Id && x.IsActive == true)
                .Select(x => x.AuthorID)
                .ToListAsync();

            var authorsToDeactivate = existingAuthorIds.Except(authorIds).ToList();
            var authorsToAdd = authorIds.Except(existingAuthorIds).ToList();

            await DeactivateChosenAuthors(book, authorsToDeactivate, _context);
            await AddNewAuthors(book, authorsToAdd, _context);
        }

        public static async Task AddNewAuthors(Book book, List<int?> authorIdsToAdd, BookStoreContext _context)
        {
            var authorsToAdd = authorIdsToAdd.Select(authorId => new BookAuthor
            {
                AuthorID = authorId,
                BookID = book.Id
            }).ToList();

            _context.BookAuthor.AddRange(authorsToAdd);
            await _context.SaveChangesAsync();
        }

        public static async Task DeactivateAllAuthors(Book book, BookStoreContext _context)
        {
            var authors = await _context.BookAuthor
                .Where(x => x.BookID == book.Id && x.IsActive == true)
                .ToListAsync();

            foreach (var author in authors)
            {
                author.IsActive = false;
            }

            await _context.SaveChangesAsync();
        }

        public static async Task DeactivateChosenAuthors(Book book, List<int?> authorIdsToDeactivate, BookStoreContext _context)
        {
            var authorsToDeactivate = await _context.BookAuthor
                .Where(x => x.BookID == book.Id && authorIdsToDeactivate.Contains(x.AuthorID) && x.IsActive == true)
                .ToListAsync();

            foreach (var author in authorsToDeactivate)
            {
                author.IsActive = false;
            }

            await _context.SaveChangesAsync();
        }
    }
}
