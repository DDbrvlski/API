using BookStoreViewModels.ViewModels.Products.DiscountCodes;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Interfaces
{
    public interface IDiscountCodeStore
    {
        Task<ActionResult<IEnumerable<DiscountCodeForView>>> GetDiscounts();
        Task<ActionResult<DiscountCodeDetailsForView>> GetDiscount(int id);
        Task<IActionResult> PostDiscount(DiscountCodePostForView entity);
        Task<IActionResult> PutDiscount(int id, [FromBody] DiscountCodePostForView entity);
        Task<IActionResult> DeleteDiscount(int id);
    }
}
