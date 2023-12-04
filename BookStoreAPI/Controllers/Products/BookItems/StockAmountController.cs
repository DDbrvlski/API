using BookStoreData.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Models.Products.BookItems;
using BookStoreViewModels.ViewModels.Products.StockAmount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Controllers.Products.BookItems
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockAmountController : CRUDController<StockAmount, StockAmountForView, StockAmountForView, StockAmountForView>
    {
        public StockAmountController(BookStoreContext context) : base(context)
        {
        }

        protected override async Task<ActionResult<IEnumerable<StockAmountForView>>> GetAllEntitiesCustomAsync()
        {
            return await _context.StockAmount
                .Include(x => x.BookItem)
                    .ThenInclude(x => x.Book)
                .Where(x => x.IsActive == true)
                .Select(x => new StockAmountForView
                {
                    Id = x.Id,
                    BookTitle = x.BookItem.Book.Title,
                    Amount = x.Amount,
                    BookItemID = x.BookItemID
                })
                .ToListAsync();
        }

        protected override async Task<ActionResult<StockAmountForView?>> GetCustomEntityByIdAsync(int id)
        {
            var element = await _context.StockAmount
                .Include(x => x.BookItem)
                    .ThenInclude(x => x.Book)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);

            return new StockAmountForView
            {
                Id = element.Id,
                BookTitle = element.BookItem.Book.Title,
                Amount = element.Amount,
                BookItemID = element.BookItemID
            };
        }
    }
}
