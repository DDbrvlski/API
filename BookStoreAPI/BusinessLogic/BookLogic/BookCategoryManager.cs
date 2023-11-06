using BookStoreData.Data;
using BookStoreData.Models.Products.Books;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.BusinessLogic.BookLogic
{
    public class BookCategoryManager
    {
        public static async Task UpdateCategories(Book book, List<int?> categoryIds, BookStoreContext _context)
        {
            var existingCategoryIds = await _context.BookCategory
                .Where(x => x.BookID == book.Id && x.IsActive == true)
                .Select(x => x.CategoryID)
                .ToListAsync();

            var categoriesToDeactivate = existingCategoryIds.Except(categoryIds).ToList();
            var categoriesToAdd = categoryIds.Except(existingCategoryIds).ToList();

            await DeactivateChosenCategories(book, categoriesToDeactivate, _context);
            await AddNewCategories(book, categoriesToAdd, _context);
        }
        public static async Task AddNewCategories(Book book, List<int?> categoryIdsToAdd, BookStoreContext _context)
        {
            var categoriesToAdd = categoryIdsToAdd.Select(categoryId => new BookCategory
            {
                CategoryID = categoryId,
                BookID = book.Id
            }).ToList();

            _context.BookCategory.AddRange(categoriesToAdd);
            await _context.SaveChangesAsync();
        }
        public static async Task DeactivateAllCategories(Book book, BookStoreContext _context)
        {
            var categories = await _context.BookCategory
                .Where(x => x.BookID == book.Id && x.IsActive == true)
                .ToListAsync();

            foreach (var category in categories)
            {
                category.IsActive = false;
            }

            await _context.SaveChangesAsync();
        }
        public static async Task DeactivateChosenCategories(Book book, List<int?> categoryIdsToDeactivate, BookStoreContext _context)
        {
            var categoriesToDeactivate = await _context.BookCategory
                .Where(x => x.BookID == book.Id && categoryIdsToDeactivate.Contains(x.CategoryID) && x.IsActive == true)
                .ToListAsync();

            foreach (var category in categoriesToDeactivate)
            {
                category.IsActive = false;
            }

            await _context.SaveChangesAsync();
        }

    }
}
