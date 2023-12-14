using BookStoreAPI.BusinessLogic.DiscountCodeLogic;
using BookStoreData.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Models.Products.BookItems;
using BookStoreViewModels.ViewModels.Products.BookItems;
using BookStoreViewModels.ViewModels.Products.DiscountCodes;
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

        protected override async Task<ActionResult<DiscountCodeDetailsForView?>> GetCustomEntityByIdAsync(int id)
        {
            var element = await _context.DiscountCode
                .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);

            return new DiscountCodeDetailsForView
            {
                Id = element.Id,
                IsAvailable = DateTime.Today >= element.StartingDate && DateTime.Today <= element.ExpiryDate.AddDays(1),
                Code = element.Code,
                Description = element.Description,
                ExpiryDate = element.ExpiryDate,
                PercentOfDiscount = element.PercentOfDiscount,
                StartingDate = element.StartingDate
            };
        }
        protected override async Task<ActionResult<IEnumerable<DiscountCodeForView>>> GetAllEntitiesCustomAsync()
        {
            return await _context.DiscountCode
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
