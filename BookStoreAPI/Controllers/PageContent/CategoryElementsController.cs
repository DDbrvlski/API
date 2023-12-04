using BookStoreAPI.BusinessLogic.PageContentLogic.CategoryElementLogic;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Data;
using BookStoreData.Models.PageContent;
using BookStoreViewModels.ViewModels.PageContent.CategoryElements;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.PageContent
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryElementsController : CRUDController<CategoryElement, CategoryElementsForView, CategoryElementsForView, CategoryElementsForView>
    {
        public CategoryElementsController(BookStoreContext context) : base(context)
        {
        }

        protected override async Task<ActionResult<CategoryElementsForView?>> GetCustomEntityByIdAsync(int id)
        {
            return await CategoryElementB.GetCategoryElementById(_context, id);
        }

        protected override async Task<ActionResult<IEnumerable<CategoryElementsForView>>> GetAllEntitiesCustomAsync()
        {
            return await CategoryElementB.GetAllCategoryElements(_context);
        }

        protected override async Task<IActionResult> CreateEntityCustomAsync(CategoryElementsForView entity)
        {
            return await CategoryElementB.ConvertEntityPostForViewAndSave<CategoryElementB>(entity, _context);
        }

        protected override async Task UpdateEntityCustomAsync(CategoryElement oldEntity, CategoryElementsForView updatedEntity)
        {
            await CategoryElementB.ConvertEntityPostForViewAndUpdate<CategoryElementB>(oldEntity, updatedEntity, _context);
        }

        protected override async Task<IActionResult> DeleteEntityCustomAsync(CategoryElement entity)
        {
            return await CategoryElementB.DeactivateEntityAndSave<CategoryElementB>(entity, _context);
        }
    }
}
