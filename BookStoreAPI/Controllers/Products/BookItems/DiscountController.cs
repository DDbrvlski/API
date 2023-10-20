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
                .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);

            return new DiscountDetailsForView
            {
                IsAvailable = DateTime.Today >= element.StartingDate && DateTime.Today <= element.ExpiryDate,
                ListOfBookItems = element.BookDiscounts
                .Where(x => x.IsActive == true)
                .Select(x => new BookItemsForView
                {
                    BookId = x.BookItem.Book.Id,
                    BookTitle = x.BookItem.Book.Title
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
                    IsAvailable = DateTime.Today >= x.StartingDate && DateTime.Today <= x.ExpiryDate,
                }.CopyProperties(x))
                .ToListAsync();
        }
        protected override async Task<IActionResult> CreateEntityCustomAsync(DiscountPostForView entity)
        {
            return await DiscountB.ConvertDiscountPostForViewAndSave(entity, _context);
        }
        protected override async Task UpdateEntityCustomAsync(Discount oldEntity, DiscountPostForView updatedEntity)
        {
            await DiscountB.ConvertDiscountPostForViewAndUpdate(oldEntity, updatedEntity, _context);
        }
        protected override async Task<IActionResult> DeleteEntityCustomAsync(Discount entity)
        {
            return await DiscountB.DeactivateDiscount(entity, _context);
        }
    }
}
