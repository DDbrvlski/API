using BookStoreAPI.Models.Products.BookItems;
using BookStoreAPI.ViewModels.Products.Discounts;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Helpers.BaseDiscountController
{
    public abstract class ABaseDiscountController : ControllerBase
    {
        protected abstract Task<ActionResult<IEnumerable<DiscountForView>>> GetAllEntitiesAsync();
        protected abstract Task<ActionResult<DiscountDetailsForView>> GetEntityByIdAsync(int id);
        protected abstract Task<IActionResult> CreateEntityAsync(DiscountPostForView entity);
        protected abstract Task<IActionResult> UpdateEntityAsync(int id, DiscountPostForView updatedEntity);
        protected abstract Task<IActionResult> DeleteEntityAsync(int id);
        protected abstract bool EntityExists(int id);

        protected virtual async Task<Discount?> GetEntityAsync(int id) { return null; }
        protected virtual async Task<DiscountDetailsForView?> GetCustomEntityByIdAsync(int id) { return null; }
        protected virtual async Task<ActionResult<IEnumerable<DiscountForView>>> GetAllEntitiesCustomAsync() { return null; }
        protected virtual async Task<IActionResult> CreateEntityCustomAsync(DiscountPostForView entity) { return null; }
        protected virtual async Task UpdateEntityCustomAsync(Discount oldEntity, DiscountPostForView updatedEntity) { }
        protected virtual async Task<IActionResult> DeleteEntityCustomAsync(Discount entity) { return null; }
    }
}
