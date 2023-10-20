using BookStoreAPI.Data;
using BookStoreAPI.Interfaces;
using BookStoreAPI.ViewModels.Products.Discounts;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Helpers.BaseDiscountController
{
    public class CRUDDiscountController : BaseDiscountController, IDiscountStore
    {
        public CRUDDiscountController(BookStoreContext context) : base(context)
        {
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscount(int id)
        {
            return await DeleteEntityAsync(id);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DiscountDetailsForView>> GetDiscount(int id)
        {
            return await GetEntityByIdAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiscountForView>>> GetDiscounts()
        {
            return await GetAllEntitiesAsync();
        }

        [HttpPost]
        public async Task<IActionResult> PostDiscount(DiscountPostForView entity)
        {
            return await CreateEntityAsync(entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiscount(int id, [FromBody] DiscountPostForView entity)
        {
            return await UpdateEntityAsync(id, entity);
        }
    }
}
