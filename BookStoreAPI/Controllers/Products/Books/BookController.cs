using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Helpers.BaseController;
using BookStoreAPI.Models.BusinessLogic.BookLogic;
using BookStoreAPI.Models.Products.Books;
using BookStoreAPI.ViewModels.Products.Books;
using BookStoreAPI.ViewModels.Products.Books.Dictionaries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Controllers.Products.Books
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : CRUDController<Book, BookPostForView, BookForView, BookDetailsForView>
    {
        public BookController(BookStoreContext context) : base(context)
        {
        }

        
        protected override async Task<BookDetailsForView?> GetCustomEntityByIdAsync(int id)
        {
            var element = await _context.Book
                .Include(x => x.OriginalLanguage)
                .Include(x => x.Publisher)
                .Include(x => x.BookAuthors)
                    .ThenInclude(x => x.Author)
                .Include(x => x.BookCategories)
                    .ThenInclude(x => x.Category)
                .Include(x => x.BookImages)
                    .ThenInclude(x => x.Image)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);

            return new BookDetailsForView
            {
                OriginalLanguageName = element.OriginalLanguage.Name,
                PublisherName = element.Publisher.Name,
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
            }.CopyProperties(element);
        }

        protected override async Task<ActionResult<IEnumerable<BookForView>>> GetAllEntitiesCustomAsync()
        {
            return await _context.Book
                .Include(x => x.OriginalLanguage)
                .Include(x => x.Publisher)
                .Include(x => x.BookAuthors)
                    .ThenInclude(x => x.Author)
                .Include(x => x.BookCategories)
                    .ThenInclude(x => x.Category)
                .Include(x => x.BookImages)
                    .ThenInclude(x => x.Image)
                .Where(x => x.IsActive == true)
                .Select(x => new BookForView
                {
                    Id = x.Id,
                    PublisherName = x.Publisher.Name,
                    Authors = x.BookAuthors
                            .Where(y => y.IsActive == true)
                            .Select(y => new AuthorsForView
                            {
                                Id = y.Author.Id,
                                Name = y.Author.Name,
                                Surname = y.Author.Surname,
                            }).ToList()
                }.CopyProperties(x))
                .ToListAsync();
        }

        protected override async Task<IActionResult> CreateEntityCustomAsync(BookPostForView entity)
        {
            return await BookB.ConvertBookPostForViewAndSave(entity, _context);
        }

        protected override async Task UpdateEntityCustomAsync(Book oldEntity, BookPostForView updatedEntity)
        {
            await BookB.ConvertBookPostForViewAndUpdate(oldEntity, updatedEntity, _context);
        }

        protected override async Task<IActionResult> DeleteEntityCustomAsync(Book entity)
        {
            return await BookB.DeactivateBook(entity, _context);
        }
    }
}
