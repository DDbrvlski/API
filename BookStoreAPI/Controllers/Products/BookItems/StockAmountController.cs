using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Helpers.BaseController;
using BookStoreAPI.Models.Products.BookItems;
using BookStoreAPI.ViewModels.Products.StockAmount;
using Microsoft.AspNetCore.Http;
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
                    BookTitle = x.BookItem.Book.Title
                }.CopyProperties(x))
                .ToListAsync();
        }

        protected override async Task<StockAmountForView?> GetCustomEntityByIdAsync(int id)
        {
            var element = await _context.StockAmount
                .Include(x => x.BookItem)
                    .ThenInclude(x => x.Book)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);

            return new StockAmountForView
            {
                Id = element.Id,
                BookTitle = element.BookItem.Book.Title
            }.CopyProperties(element);
        }
    }
}
