using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Products.Books;
using BookStoreAPI.ViewModels.PageContent;
using BookStoreAPI.ViewModels.Products.Books;
using BookStoreAPI.ViewModels.Products.Books.Dictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace BookStoreAPI.Controllers.Products.Books
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : CRUDController<Book>
    {
        public BookController(BookStoreContext context) : base(context)
        {
        }

        [HttpGet("full-details")]
        public async Task<ActionResult<IEnumerable<BookDetailsForView>>> GetAllPropertiesFromEntities()
        {
            return await GetAllPropertiesFromEntitiesAsync();
        }

        [HttpGet("full-details/{id}")]
        public async Task<ActionResult<BookDetailsForView>> GetAllPropertiesFromEntity(int id)
        {
            return await GetAllPropertiesFromEntityByIdAsync(id);
        }

        protected async Task<BookDetailsForView> GetAllPropertiesFromEntityByIdAsync(int id)
        {
            var element = await _context.Book
                .Include(x => x.OriginalLanguage)
                .Include(x => x.Publisher)
                .Include(x => x.BookAuthors)
                    .ThenInclude(x => x.Author)
                .Include(x => x.BookCategories)
                    .ThenInclude(x => x.Category)
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
            }.CopyProperties(element);
        }

        protected async Task<ActionResult<IEnumerable<BookDetailsForView>>> GetAllPropertiesFromEntitiesAsync()
        {
            return await _context.Book
                .Include(x => x.OriginalLanguage)
                .Include(x => x.Publisher)
                .Include(x => x.BookAuthors)
                    .ThenInclude(x => x.Author)
                .Include(x => x.BookCategories)
                    .ThenInclude(x => x.Category)
                .Where(x => x.IsActive == true)
                .Select(x => new BookDetailsForView
                    {
                        Id = x.Id,
                        OriginalLanguageName = x.OriginalLanguage.Name,
                        PublisherName = x.Publisher.Name,
                        Categories = x.BookCategories
                            .Where(y => y.IsActive == true)
                            .Select(y => new CategoryForView
                            {
                                Id = y.Category.Id,
                                Name = y.Category.Name,
                            }).ToList(),
                        Authors = x.BookAuthors
                            .Where(y => y.IsActive == true)
                            .Select(y => new AuthorsForView
                            {
                                Id = y.Author.Id,
                                Name = y.Author.Name,
                                Surname = y.Author.Surname,
                            }).ToList(),
                }.CopyProperties(x))
                .ToListAsync();
        }
    }
}
