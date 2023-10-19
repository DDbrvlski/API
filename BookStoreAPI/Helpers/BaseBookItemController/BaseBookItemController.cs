using BookStoreAPI.Data;
using BookStoreAPI.Models.BusinessLogic.BookItemsLogic;
using BookStoreAPI.Models.Products.BookItems;
using BookStoreAPI.Models.Products.Books;
using BookStoreAPI.ViewModels.Products.BookItems;
using BookStoreAPI.ViewModels.Products.Books.Dictionaries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Helpers.BaseBookItemController
{
    public class BaseBookItemController : ABaseBookItemController
    {
        protected readonly BookStoreContext _context;
        public BaseBookItemController(BookStoreContext context)
        {
            _context = context;
        }
        protected override async Task<IActionResult> CreateEntityAsync(BookItemsPostForView entity)
        {
            return await CreateEntityCustomAsync(entity);
        }

        protected override async Task<IActionResult> DeleteEntityAsync(int id)
        {
            var entity = await GetEntityAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return await DeleteEntityCustomAsync(entity);
        }

        protected override bool EntityExists(int id)
        {
            return _context.BookItem.Find(id) != null;
        }

        protected override async Task<ActionResult<IEnumerable<BookItemsForView>>> GetAllEntitiesAsync()
        {
            return await GetAllEntitiesCustomAsync();
        }

        protected override async Task<ActionResult<BookItemsDetailsForView>> GetEntityByIdAsync(int id)
        {
            return await GetEntityByIdAsync(id);
        }

        protected override async Task<IActionResult> UpdateEntityAsync(int id, BookItemsPostForView updatedEntity)
        {
            if (id != updatedEntity.Id)
            {
                return BadRequest("Invalid data.");
            }

            var entity = await GetEntityAsync(id);

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

        protected override async Task<BookItem?> GetEntityAsync(int id)
        {
            return await _context.BookItem.FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
        }

        protected override async Task<BookItemsDetailsForView?> GetCustomEntityByIdAsync(int id) 
        {
            var element = await _context.BookItem
                .Include(x => x.Translator)
                .Include(x => x.Language)
                .Include(x => x.Edition)
                .Include(x => x.FileFormat)
                .Include(x => x.Form)
                .Include(x => x.Availability)
                .Include(x => x.Book)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);

            return new BookItemsDetailsForView
            {
                TranslatorName = element.Translator.Name + " " + element.Translator.Surname,
                LanguageName = element.Language.Name,
                EditionName = element.Edition.Name,
                FileFormatName = element.FileFormat.Name,
                FormName = element.Form.Name,
                AvailabilityName = element.Availability.Name,
                BookId = (int)element.BookID,
                BookName = element.Book.Title
            }.CopyProperties(element);
        }
        protected override async Task<ActionResult<IEnumerable<BookItemsForView>>> GetAllEntitiesCustomAsync() 
        {
            return await _context.BookItem
                .Include(x => x.Book)
                .Where(x => x.IsActive == true)
                .Select(x => new BookItemsForView
                {
                    BookId = (int)x.BookID,
                    BookTitle = x.Book.Title
                }.CopyProperties(x))
                .ToListAsync();
        }
        protected override async Task<IActionResult> CreateEntityCustomAsync(BookItemsPostForView entity) 
        {
            return await BookItemB.ConvertBookItemPostForViewAndSave(entity, _context);
        }
        protected override async Task UpdateEntityCustomAsync(BookItem oldEntity, BookItemsPostForView updatedEntity) 
        {
            await BookItemB.ConvertBookItemPostForViewAndUpdate(oldEntity, updatedEntity, _context);
        }
        protected override async Task<IActionResult> DeleteEntityCustomAsync(BookItem entity) 
        {
            return await BookItemB.DeactivateBookItem(entity, _context);
        }
    }
}
