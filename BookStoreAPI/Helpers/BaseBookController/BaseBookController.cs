using BookStoreAPI.Data;
using BookStoreAPI.Models.BusinessLogic.BookLogic;
using BookStoreAPI.Models.Helpers;
using BookStoreAPI.Models.Products.Books;
using BookStoreAPI.ViewModels.Products.Books;
using BookStoreAPI.ViewModels.Products.Books.Dictionaries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Helpers.BaseBookController
{
    public class BaseBookController : ABaseBookController
    {
        protected readonly BookStoreContext _context;
        public BaseBookController(BookStoreContext context)
        {
            _context = context;
        }
        protected override async Task<IActionResult> CreateEntityAsync(BookPostForView entity)
        {
            return await CreateEntityCustomAsync(entity);
        }

        protected override async Task<IActionResult> DeleteEntityAsync(int id)
        {
            var entity = await GetEntityByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return await DeleteEntityCustomAsync(entity);
        }

        protected override async Task<ActionResult<IEnumerable<BookDetailsForView>>> GetAllEntitiesAsync()
        {
            return await GetAllEntitiesCustomAsync();
        }

        protected override async Task<IActionResult> UpdateEntityAsync(int id, BookPostForView updatedEntity)
        {
            if (id != updatedEntity.Id)
            {
                return BadRequest("Invalid data.");
            }

            var entity = await GetEntityByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            await UpdateEntityCustomAsync(entity, updatedEntity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(updatedEntity);
        }

        protected override bool EntityExists(int id)
        {
            return _context.Book.Find(id) != null;
        }

        protected override async Task<Book?> GetEntityByIdAsync(int id)
        {
            return await _context.Book.FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
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

        protected override async Task<ActionResult<IEnumerable<BookDetailsForView>>> GetAllEntitiesCustomAsync()
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
                    Images = x.BookImages
                            .Where(y => y.IsActive == true)
                            .Select(y => new ImagesForView
                            {
                                Id = y.Image.Id,
                                Title = y.Image.Title,
                                ImageURL = y.Image.ImageURL,
                            }).ToList(),
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
