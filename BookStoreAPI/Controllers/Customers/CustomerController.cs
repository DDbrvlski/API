using BookStoreAPI.BusinessLogic.CustomerLogic;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Data;
using BookStoreData.Models.Customers;
using BookStoreViewModels.ViewModels.Customers;
using BookStoreViewModels.ViewModels.Customers.Address;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Controllers.Customers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : CRUDController<Customer, CustomerPostForView, CustomerForView, CustomerDetailsForView>
    {
        public CustomerController(BookStoreContext context) : base(context)
        {
        }

        protected override async Task<ActionResult<CustomerDetailsForView?>> GetCustomEntityByIdAsync(int id)
        {
            return await CustomerB.GetCustomerByIdAsync(id, _context);
        }
        protected override async Task<ActionResult<IEnumerable<CustomerForView>>> GetAllEntitiesCustomAsync()
        {
            return await CustomerB.GetAllCustomersAsync(_context);
        }
        protected override async Task<IActionResult> CreateEntityCustomAsync(CustomerPostForView entity)
        {
            return await CustomerB.ConvertEntityPostForViewAndSave<CustomerB>(entity, _context);
        }
        protected override async Task UpdateEntityCustomAsync(Customer oldEntity, CustomerPostForView updatedEntity)
        {
            await CustomerB.ConvertEntityPostForViewAndUpdate<CustomerB>(oldEntity, updatedEntity, _context);
        }
        protected override async Task<IActionResult> DeleteEntityCustomAsync(Customer entity)
        {
            return await CustomerB.DeactivateEntityAndSave<CustomerB>(entity, _context);
        }
    }
}
