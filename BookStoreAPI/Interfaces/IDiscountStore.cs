using BookStoreAPI.ViewModels.Products.BookItems;
using BookStoreAPI.ViewModels.Products.Discounts;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Interfaces
{
    public interface IDiscountStore
    {
        Task<ActionResult<IEnumerable<DiscountForView>>> GetDiscounts();
        Task<ActionResult<DiscountDetailsForView>> GetDiscount(int id);
        Task<IActionResult> PostDiscount(DiscountPostForView entity);
        Task<IActionResult> PutDiscount(int id, [FromBody] DiscountPostForView entity);
        Task<IActionResult> DeleteDiscount(int id);
    }
}
