using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Helpers.BaseController;
using BookStoreAPI.Models.BusinessLogic.DiscountCodeCodeLogic;
using BookStoreAPI.Models.BusinessLogic.DiscountLogic;
using BookStoreAPI.Models.Products.BookItems;
using BookStoreAPI.ViewModels.Products.BookItems;
using BookStoreAPI.ViewModels.Products.DiscountCodes;
using BookStoreAPI.ViewModels.Products.Discounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Controllers.Products.BookItems
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountCodesController : CRUDController<DiscountCode, DiscountCodePostForView, DiscountCodeForView, DiscountCodeDetailsForView>
    {
        public DiscountCodesController(BookStoreContext context) : base(context)
        {
        }

        protected override async Task<DiscountCodeDetailsForView?> GetCustomEntityByIdAsync(int id)
        {
            var element = await _context.DiscountCode
                .Include(x => x.BookDiscountCodes)
                    .ThenInclude(x => x.BookItem)
                    .ThenInclude(x => x.Book)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);

            return new DiscountCodeDetailsForView
            {
                Id = element.Id,
                IsAvailable = DateTime.Today >= element.StartingDate && DateTime.Today <= element.ExpiryDate.AddDays(1),
                Code = element.Code,
                Description = element.Description,
                ExpiryDate = element.ExpiryDate,
                PercentOfDiscount = element.PercentOfDiscount,
                StartingDate = element.StartingDate,
                ListOfBookItems = element.BookDiscountCodes
                .Where(x => x.IsActive == true)
                .Select(x => new BookItemsForDiscountForView
                {
                    Id = x.Id,
                    BookItemID = x.BookItemID,
                    BookID = x.BookItem.BookID,
                    BookTitle = x.BookItem.Book.Title,
                    ISBN = x.BookItem.ISBN,
                    FormName = x.BookItem.Form.Name,
                    NettoPrice = x.BookItem.NettoPrice,
                }).ToList()
            };
        }
        protected override async Task<ActionResult<IEnumerable<DiscountCodeForView>>> GetAllEntitiesCustomAsync()
        {
            return await _context.DiscountCode
                .Include(x => x.BookDiscountCodes)
                    .ThenInclude(x => x.BookItem)
                    .ThenInclude(x => x.Book)
                .Where(x => x.IsActive == true)
                .Select(x => new DiscountCodeForView
                {
                    Id = x.Id,
                    IsAvailable = DateTime.Today >= x.StartingDate && DateTime.Today <= x.ExpiryDate.AddDays(1),
                    Code = x.Code,
                    Description = x.Description,
                    PercentOfDiscount = x.PercentOfDiscount,
                })
                .ToListAsync();
        }
        protected override async Task<IActionResult> CreateEntityCustomAsync(DiscountCodePostForView entity)
        {
            return await DiscountCodeB.ConvertEntityPostForViewAndSave<DiscountCodeB>(entity, _context);
        }
        protected override async Task UpdateEntityCustomAsync(DiscountCode oldEntity, DiscountCodePostForView updatedEntity)
        {
            await DiscountCodeB.ConvertEntityPostForViewAndUpdate<DiscountCodeB>(oldEntity, updatedEntity, _context);
        }
        protected override async Task<IActionResult> DeleteEntityCustomAsync(DiscountCode entity)
        {
            return await DiscountCodeB.DeactivateEntityAndSave<DiscountCodeB>(entity, _context);
        }
    }
}
