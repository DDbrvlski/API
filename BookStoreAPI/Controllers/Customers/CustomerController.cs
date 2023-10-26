using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Helpers.BaseController;
using BookStoreAPI.Models.BusinessLogic.BookItemsLogic;
using BookStoreAPI.Models.Customers;
using BookStoreAPI.Models.Products.BookItems;
using BookStoreAPI.ViewModels.Customers;
using BookStoreAPI.ViewModels.Customers.Address;
using BookStoreAPI.ViewModels.Products.BookItems;
using BookStoreAPI.ViewModels.Products.Books.Dictionaries;
using CustomerStoreAPI.Models.BusinessLogic.CustomerLogic;
using Microsoft.AspNetCore.Http;
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

        protected override async Task<CustomerDetailsForView?> GetCustomEntityByIdAsync(int id)
        {
            var element = await _context.Customer
                .Include(x => x.Gender)
                .Include(x => x.CustomerAddresses)
                    .ThenInclude(x => x.Address)
                    .ThenInclude(x => x.City)
                .Include(x => x.CustomerAddresses)
                    .ThenInclude(x => x.Address)
                    .ThenInclude(x => x.Country)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);

            return new CustomerDetailsForView
            {
                Id = element.Id,
                GenderName = element.Gender.Name,
                ListOfCustomerAdresses = element.CustomerAddresses
                            .Where(z => z.IsActive == true)
                            .Select(y => new AddressForView
                            {
                                Id = y.Address.Id,
                                Street = y.Address.Street,
                                StreetNumber = y.Address.StreetNumber,
                                HouseNumber = y.Address.HouseNumber,
                                Postcode = y.Address.Postcode,
                                CityID = y.Address.CityID,
                                CityName = y.Address.City.Name,
                                CountryID = y.Address.CountryID,
                                CountryName = y.Address.Country.Name
                            }).ToList(),

            }.CopyProperties(element);
        }
        protected override async Task<ActionResult<IEnumerable<CustomerForView>>> GetAllEntitiesCustomAsync()
        {
            return await _context.Customer
                .Include(x => x.Gender)
                .Include(x => x.CustomerAddresses)
                    .ThenInclude(x => x.Address)
                .Where(x => x.IsActive == true)
                .Select(x => new CustomerForView
                {
                    Id = x.Id,
                    
                }.CopyProperties(x))
                .ToListAsync();
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
