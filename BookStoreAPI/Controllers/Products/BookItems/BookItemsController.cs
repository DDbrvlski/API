using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Helpers.BaseController;
using BookStoreAPI.Models.BusinessLogic.BookItemsLogic;
using BookStoreAPI.Models.Products.BookItems;
using BookStoreAPI.ViewModels.Products.BookItems;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Controllers.Products.BookItems
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookItemsController : CRUDController<BookItem, BookItemsPostForView, BookItemsForView, BookItemsDetailsForView>
    {
        public BookItemsController(BookStoreContext context) : base(context)
        {
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
                .Include(x => x.BookDiscounts)
                    .ThenInclude(x => x.Discount)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);

            return new BookItemsDetailsForView
            {
                Id = element.Id,
                TranslatorName = element.Translator.Name + " " + element.Translator.Surname,
                LanguageName = element.Language.Name,
                EditionName = element.Edition.Name,
                FileFormatName = element.FileFormat.Name,
                FormName = element.Form.Name,
                AvailabilityName = element.Availability.Name,
                BookName = element.Book.Title,
                BruttoPrice = element.NettoPrice * (1 + (decimal)(element.VAT / 100.0f))
            }.CopyProperties(element);
        }
        protected override async Task<ActionResult<IEnumerable<BookItemsForView>>> GetAllEntitiesCustomAsync()
        {
            return await _context.BookItem
                .Include(x => x.Book)
                .Include(x => x.Form)
                .Where(x => x.IsActive == true)
                .Select(x => new BookItemsForView
                {
                    Id = x.Id,
                    FormName = x.Form.Name,
                    BookTitle = x.Book.Title
                }.CopyProperties(x))
                .ToListAsync();
        }
        protected override async Task<IActionResult> CreateEntityCustomAsync(BookItemsPostForView entity)
        {
            return await BookItemB.ConvertEntityPostForViewAndSave(entity, _context);
        }
        protected override async Task UpdateEntityCustomAsync(BookItem oldEntity, BookItemsPostForView updatedEntity)
        {
            await BookItemB.ConvertEntityPostForViewAndUpdate(oldEntity, updatedEntity, _context);
        }
        protected override async Task<IActionResult> DeleteEntityCustomAsync(BookItem entity)
        {
            return await BookItemB.DeactivateEntityAndSave(entity, _context);
        }
    }
}
