using BookStoreAPI.Data;
using BookStoreAPI.Models.BusinessLogic.DiscountLogic;
using BookStoreAPI.Models.Products.BookItems;
using BookStoreAPI.ViewModels.Products.BookItems;
using BookStoreAPI.ViewModels.Products.Discounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace BookStoreAPI.Helpers.BaseDiscountController
{
    public class BaseDiscountController : ABaseDiscountController
    {
        protected readonly BookStoreContext _context;
        public BaseDiscountController(BookStoreContext context)
        {
            _context = context;
        }
        protected override async Task<IActionResult> CreateEntityAsync(DiscountPostForView entity)
        {
            return await CreateEntityCustomAsync(entity);
        }
        protected override async Task<IActionResult> DeleteEntityAsync(int id)
        {
            var entity = await GetEntityAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return await DeleteEntityCustomAsync(entity);
        }
        protected override bool EntityExists(int id)
        {
            return _context.Discount.Find(id) != null;
        }
        protected override async Task<ActionResult<IEnumerable<DiscountForView>>> GetAllEntitiesAsync()
        {
            return await GetAllEntitiesCustomAsync();
        }
        protected override async Task<ActionResult<DiscountDetailsForView>> GetEntityByIdAsync(int id)
        {
            return await GetEntityByIdAsync(id);
        }
        protected override async Task<IActionResult> UpdateEntityAsync(int id, DiscountPostForView updatedEntity)
        {
            if (id != updatedEntity.Id)
            {
                return BadRequest("Invalid data.");
            }

            var entity = await GetEntityAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            await UpdateEntityCustomAsync(entity, updatedEntity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(updatedEntity);
        }

        protected override async Task<Discount?> GetEntityAsync(int id) 
        { 
            return await _context.Discount.FirstOrDefaultAsync(x => x.Id == id && x.IsActive); 
        }
        protected override async Task<DiscountDetailsForView?> GetCustomEntityByIdAsync(int id) 
        {
            var element = await _context.Discount
                .Include(x => x.BookDiscounts)
                    .ThenInclude(x => x.BookItem)
                    .ThenInclude(x => x.Book)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);

            return new DiscountDetailsForView
            {
                IsAvailable = DateTime.Today >= element.StartingDate && DateTime.Today <= element.ExpiryDate,
                ListOfBookItems = element.BookDiscounts
                .Where(x => x.IsActive == true)
                .Select(x => new BookItemsForView
                {
                    BookId = x.BookItem.Book.Id,
                    BookTitle = x.BookItem.Book.Title                    
                }.CopyProperties(x)).ToList()
            }.CopyProperties(element);
        }
        protected override async Task<ActionResult<IEnumerable<DiscountForView>>> GetAllEntitiesCustomAsync() 
        {
            return await _context.Discount
                .Include(x => x.BookDiscounts)
                    .ThenInclude(x => x.BookItem)
                    .ThenInclude(x => x.Book)
                .Where(x => x.IsActive == true)
                .Select(x => new DiscountForView
                    {
                        IsAvailable = DateTime.Today >= x.StartingDate && DateTime.Today <= x.ExpiryDate,
                    }.CopyProperties(x))
                .ToListAsync();
        }
        protected override async Task<IActionResult> CreateEntityCustomAsync(DiscountPostForView entity) 
        {
            return await DiscountB.ConvertDiscountPostForViewAndSave(entity, _context);
        }
        protected override async Task UpdateEntityCustomAsync(Discount oldEntity, DiscountPostForView updatedEntity) 
        {
            await DiscountB.ConvertDiscountPostForViewAndUpdate(oldEntity, updatedEntity, _context);
        }
        protected override async Task<IActionResult> DeleteEntityCustomAsync(Discount entity) 
        {
            return await DiscountB.DeactivateDiscount(entity, _context);
        }
    }
}
