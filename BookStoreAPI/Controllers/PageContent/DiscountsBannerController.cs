using BookStoreAPI.BusinessLogic.PageContentLogic.DiscountsBannerLogic;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Data;
using BookStoreData.Models.PageContent;
using BookStoreViewModels.ViewModels.PageContent.DiscountsBanner;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.PageContent
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsBannerController : CRUDController<DiscountsBanner, DiscountsBannerForView, DiscountsBannerForView, DiscountsBannerForView>
    {
        public DiscountsBannerController(BookStoreContext context) : base(context)
        {
        }

        protected override async Task<ActionResult<DiscountsBannerForView?>> GetCustomEntityByIdAsync(int id)
        {
            return await DiscountsBannerB.GetDiscountsBannerById(_context, id);
        }

        protected override async Task<ActionResult<IEnumerable<DiscountsBannerForView>>> GetAllEntitiesCustomAsync()
        {
            return await DiscountsBannerB.GetAllDiscountsBanners(_context);
        }

        protected override async Task<IActionResult> CreateEntityCustomAsync(DiscountsBannerForView entity)
        {
            return await DiscountsBannerB.ConvertEntityPostForViewAndSave<DiscountsBannerB>(entity, _context);
        }

        protected override async Task UpdateEntityCustomAsync(DiscountsBanner oldEntity, DiscountsBannerForView updatedEntity)
        {
            await DiscountsBannerB.ConvertEntityPostForViewAndUpdate<DiscountsBannerB>(oldEntity, updatedEntity, _context);
        }

        protected override async Task<IActionResult> DeleteEntityCustomAsync(DiscountsBanner entity)
        {
            return await DiscountsBannerB.DeactivateEntityAndSave<DiscountsBannerB>(entity, _context);
        }
    }
}
