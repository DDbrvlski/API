using BookStoreAPI.BusinessLogic.BookLogic;
using BookStoreAPI.BusinessLogic.PageContentLogic.BannerLogic;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Data;
using BookStoreData.Models.PageContent;
using BookStoreData.Models.Products.Books;
using BookStoreViewModels.ViewModels.PageContent.Banner;
using BookStoreViewModels.ViewModels.Products.Books;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.PageContent
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerController : CRUDController<Banner, BannerForView, BannerForView, BannerForView>
    {
        public BannerController(BookStoreContext context) : base(context)
        {
        }

        protected override async Task<ActionResult<BannerForView?>> GetCustomEntityByIdAsync(int id)
        {
            return await BannerB.GetBannerById(_context, id);
        }

        protected override async Task<ActionResult<IEnumerable<BannerForView>>> GetAllEntitiesCustomAsync()
        {
            return await BannerB.GetAllBanners(_context);
        }

        protected override async Task<IActionResult> CreateEntityCustomAsync(BannerForView entity)
        {
            return await BannerB.ConvertEntityPostForViewAndSave<BannerB>(entity, _context);
        }

        protected override async Task UpdateEntityCustomAsync(Banner oldEntity, BannerForView updatedEntity)
        {
            await BannerB.ConvertEntityPostForViewAndUpdate<BannerB>(oldEntity, updatedEntity, _context);
        }

        protected override async Task<IActionResult> DeleteEntityCustomAsync(Banner entity)
        {
            return await BannerB.DeactivateEntityAndSave<BannerB>(entity, _context);
        }
    }
}
