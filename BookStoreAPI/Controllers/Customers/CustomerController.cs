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

        protected override async Task<CustomerDetailsForView?> GetCustomEntityByIdAsync(int id)
        {
            return await _context.Customer
                .Include(x => x.CustomerAddresses)
                    .ThenInclude(x => x.Address)
                    .ThenInclude(x => x.City)
                .Include(x => x.CustomerAddresses)
                    .ThenInclude(x => x.Address)
                    .ThenInclude(x => x.Country)
                .Where(x => x.Id == id && x.IsActive)
                .Select(element => new CustomerDetailsForView()
                {
                    Id = element.Id,
                    IsSubscribed = (bool)element.IsSubscribed,
                    Name = element.Name,
                    Surname = element.Surname,
                    ListOfCustomerAdresses = element.CustomerAddresses
                            .Where(z => z.IsActive == true)
                            .Select(y => new AddressDetailsForView
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

                }).FirstAsync();
        }
        protected override async Task<ActionResult<IEnumerable<CustomerForView>>> GetAllEntitiesCustomAsync()
        {
            return await _context.Customer
                .Include(x => x.CustomerAddresses)
                    .ThenInclude(x => x.Address)
                .Where(x => x.IsActive == true)
                .Select(x => new CustomerForView
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                })
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
