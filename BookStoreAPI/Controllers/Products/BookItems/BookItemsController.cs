using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Helpers.BaseController;
using BookStoreAPI.Models.BusinessLogic.BookItemsLogic;
using BookStoreAPI.Models.BusinessLogic.BookLogic;
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
            return await _context.BookItem
                .Include(x => x.Translator)
                .Include(x => x.Language)
                .Include(x => x.Edition)
                .Include(x => x.FileFormat)
                .Include(x => x.Form)
                .Include(x => x.Availability)
                .Include(x => x.Book)
                .Include(x => x.BookDiscounts)
                    .ThenInclude(x => x.Discount)
                .Select(element => new BookItemsDetailsForView
                {
                    Id = element.Id,
                    TranslatorName = element.Translator.Name + " " + element.Translator.Surname,
                    LanguageName = element.Language.Name,
                    EditionName = element.Edition.Name,
                    FileFormatName = element.FileFormat.Name,
                    FormName = element.Form.Name,
                    AvailabilityName = element.Availability.Name,
                    BookName = element.Book.Title,
                    BruttoPrice = element.NettoPrice * (1 + (decimal)(element.VAT / 100.0f)),
                    NettoPrice = element.NettoPrice,
                    VAT = element.VAT,
                    ISBN = element.ISBN,
                    Pages = element.Pages,
                    PublishingDate = element.PublishingDate,
                    TranslatorID = element.TranslatorID,
                    LanguageID = element.TranslatorID,
                    EditionID = element.EditionID,
                    BookID = element.BookID,
                    FileFormatID = element.FileFormatID,
                    FormID = element.FormID,
                    AvailabilityID = element.AvailabilityID,
                })
                .FirstAsync();
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
                    BookTitle = x.Book.Title,
                    ISBN = x.ISBN,
                    BookID = x.BookID,
                    NettoPrice = x.NettoPrice
                })
                .ToListAsync();
        }
        protected override async Task<IActionResult> CreateEntityCustomAsync(BookItemsPostForView entity)
        {
            return await BookItemB.ConvertEntityPostForViewAndSave<BookItemB>(entity, _context);
        }
        protected override async Task UpdateEntityCustomAsync(BookItem oldEntity, BookItemsPostForView updatedEntity)
        {
            await BookItemB.ConvertEntityPostForViewAndUpdate<BookItemB>(oldEntity, updatedEntity, _context);
        }
        protected override async Task<IActionResult> DeleteEntityCustomAsync(BookItem entity)
        {
            return await BookItemB.DeactivateEntityAndSave<BookItemB>(entity, _context);
        }
    }
}
