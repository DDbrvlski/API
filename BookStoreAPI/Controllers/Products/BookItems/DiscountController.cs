using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Helpers.BaseController;
using BookStoreAPI.Models.BusinessLogic.DiscountLogic;
using BookStoreAPI.Models.Products.BookItems;
using BookStoreAPI.ViewModels.Products.BookItems;
using BookStoreAPI.ViewModels.Products.Discounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Controllers.Products.BookItems
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : CRUDController<Discount, DiscountPostForView, DiscountForView, DiscountDetailsForView>
    {
        public DiscountController(BookStoreContext context) : base(context)
        {
        }

        protected override async Task<DiscountDetailsForView?> GetCustomEntityByIdAsync(int id)
        {
            var element = await _context.Discount
                .Include(x => x.BookDiscounts)
                    .ThenInclude(x => x.BookItem)
                    .ThenInclude(x => x.Book)
                .Include(x => x.BookDiscounts)
                    .ThenInclude(x => x.BookItem)
                    .ThenInclude(x => x.Form)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);

            return new DiscountDetailsForView
            {
                Id = element.Id,
                IsAvailable = DateTime.Today >= element.StartingDate && DateTime.Today <= element.ExpiryDate.AddDays(1),
                ListOfBookItems = element.BookDiscounts
                .Where(x => x.IsActive == true)
                .Select(x => new BookItemsForDiscountForView
                {
                    Id = x.Id,
                    BookItemID = x.BookItem.Id,
                    BookID = x.BookItem.BookID,
                    BookTitle = x.BookItem.Book.Title,
                    ISBN = x.BookItem.ISBN,
                    FormName = x.BookItem.Form.Name,
                    NettoPrice = x.BookItem.NettoPrice
                }.CopyProperties(x)).ToList()
            }.CopyProperties(element);
        }
        protected override async Task<ActionResult<IEnumerable<DiscountForView>>> GetAllEntitiesCustomAsync()
        {
            return await _context.Discount
                .Include(x => x.BookDiscounts)
                    .ThenInclude(x => x.BookItem)
                    .ThenInclude(x => x.Book)
                .Where(x => x.IsActive == true)
                .Select(x => new DiscountForView
                {
                    Id = x.Id,
                    IsAvailable = DateTime.Today >= x.StartingDate && DateTime.Today <= x.ExpiryDate.AddDays(1),
                }.CopyProperties(x))
                .ToListAsync();
        }
        protected override async Task<IActionResult> CreateEntityCustomAsync(DiscountPostForView entity)
        {
            return await DiscountB.ConvertEntityPostForViewAndSave<DiscountB>(entity, _context);
        }
        protected override async Task UpdateEntityCustomAsync(Discount oldEntity, DiscountPostForView updatedEntity)
        {
            await DiscountB.ConvertEntityPostForViewAndUpdate<DiscountB>(oldEntity, updatedEntity, _context);
        }
        protected override async Task<IActionResult> DeleteEntityCustomAsync(Discount entity)
        {
            return await DiscountB.DeactivateEntityAndSave<DiscountB>(entity, _context);
        }
    }
}
