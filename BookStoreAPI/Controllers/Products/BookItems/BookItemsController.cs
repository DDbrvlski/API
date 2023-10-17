using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Products.BookItems;
using BookStoreAPI.ViewModels.Products.Books.Dictionaries;
using BookStoreAPI.ViewModels.Products.Books;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreAPI.ViewModels.Products.BookItems;
using System.Xml.Linq;

namespace BookStoreAPI.Controllers.Products.BookItems
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookItemsController : CRUDController<BookItem>
    {
        public BookItemsController(BookStoreContext context) : base(context)
        {
        }

        [HttpGet("full-details")]
        public async Task<ActionResult<IEnumerable<BookItemsDetailsForView>>> GetAllPropertiesFromEntities()
        {
            return await GetAllPropertiesFromEntitiesAsync();
        }

        [HttpGet("full-details/{id}")]
        public async Task<ActionResult<BookItemsDetailsForView>> GetAllPropertiesFromEntity(int id)
        {
            return await GetAllPropertiesFromEntityByIdAsync(id);
        }

        protected async Task<BookItemsDetailsForView> GetAllPropertiesFromEntityByIdAsync(int id)
        {
            var element = await _context.BookItem
                .Include(x => x.Translator)
                .Include(x => x.Language)
                .Include(x => x.Edition)
                .Include(x => x.FileFormat)
                .Include(x => x.Form)
                .Include(x => x.Availability)
                .Include(x => x.Book)
                    .ThenInclude(x => x.OriginalLanguage)
                .Include(x => x.Book)
                    .ThenInclude(x => x.Publisher)
                .Include(x => x.Book)
                    .ThenInclude(x => x.BookCategories)
                    .ThenInclude(x => x.Category)
                .Include(x => x.Book)
                    .ThenInclude(x => x.BookAuthors)
                    .ThenInclude(x => x.Author)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);

            return new BookItemsDetailsForView
            {
                TranslatorName = element.Translator.Name + " " + element.Translator.Surname,
                LanguageName = element.Language.Name,
                EditionName = element.Edition.Name,
                FileFormatName = element.FileFormat.Name,
                FormName = element.Form.Name,
                AvailabilityName = element.Availability.Name,
                BookOriginalLanguageName = element.Book.OriginalLanguage.Name,
                BookPublisherName = element.Book.Publisher.Name,
                BookCategories = element.Book.BookCategories
                            .Where(z => z.IsActive == true)
                            .Select(y => new CategoryForView
                            {
                                Id = y.Category.Id,
                                Name = y.Category.Name,
                            }).ToList(),
                BookAuthors = element.Book.BookAuthors
                            .Where(z => z.IsActive == true)
                            .Select(y => new AuthorsForView
                            {
                                Id = y.Author.Id,
                                Name = y.Author.Name,
                                Surname = y.Author.Surname,
                            }).ToList(),
            }.CopyProperties(element);
        }

        protected async Task<ActionResult<IEnumerable<BookItemsDetailsForView>>> GetAllPropertiesFromEntitiesAsync()
        {
            return await _context.BookItem
                .Include(x => x.Translator)
                .Include(x => x.Language)
                .Include(x => x.Edition)
                .Include(x => x.FileFormat)
                .Include(x => x.Form)
                .Include(x => x.Availability)
                .Include(x => x.Book)
                    .ThenInclude(x => x.OriginalLanguage)
                .Include(x => x.Book)
                    .ThenInclude(x => x.Publisher)
                .Include(x => x.Book)
                    .ThenInclude(x => x.BookCategories)
                    .ThenInclude(x => x.Category)
                .Include(x => x.Book)
                    .ThenInclude(x => x.BookAuthors)
                    .ThenInclude(x => x.Author)
                .Where(x => x.IsActive == true)
                .Select(x => new BookItemsDetailsForView
                {
                    TranslatorName = x.Translator.Name + " " + x.Translator.Surname,
                    LanguageName = x.Language.Name,
                    EditionName = x.Edition.Name,
                    FileFormatName = x.FileFormat.Name,
                    FormName = x.Form.Name,
                    AvailabilityName = x.Availability.Name,
                    BookOriginalLanguageName = x.Book.OriginalLanguage.Name,
                    BookPublisherName = x.Book.Publisher.Name,
                    BookCategories = x.Book.BookCategories
                            .Where(z => z.IsActive == true)
                            .Select(y => new CategoryForView
                            {
                                Id = y.Category.Id,
                                Name = y.Category.Name,
                            }).ToList(),
                    BookAuthors = x.Book.BookAuthors
                            .Where(z => z.IsActive == true)
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
