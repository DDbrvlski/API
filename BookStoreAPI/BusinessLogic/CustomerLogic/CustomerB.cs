using BookStoreAPI.Helpers.BaseBusinessLogic;
using BookStoreData.Data;
using BookStoreData.Models.Customers;
using BookStoreViewModels.ViewModels.Customers;
using BookStoreViewModels.ViewModels.Customers.Address;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.BusinessLogic.CustomerLogic
{
    public class CustomerB : BaseBusinessLogic<Customer, CustomerPostForView>
    {
        protected override async Task ConvertListsToUpdate(Customer entity, CustomerPostForView entityWithData, BookStoreContext context)
        {
            //await DatabaseOperationHandler.TryToSaveChangesAsync(context);
            List<AddressPostForView> addresses = entityWithData.ListOfCustomerAdresses.ToList();

            await UpdateAllConnectedEntitiesLists(entity, addresses, context);
        }

        protected override async Task DeactivateAllConnectedEntities(Customer entity, BookStoreContext context)
        {
            await CustomerAddressManager.DeactivateAllAddresses(entity, context);
        }

        private static async Task UpdateAllConnectedEntitiesLists(Customer customer, List<AddressPostForView?> addresses, BookStoreContext _context)
        {
            await CustomerAddressManager.UpdateAddresses(customer, addresses, _context);
        }

        public static async Task<ActionResult<CustomerDetailsForView?>> GetCustomerByIdAsync(int id, BookStoreContext context)
        {
            return await context.Customer
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

        public static async Task<ActionResult<IEnumerable<CustomerForView>>> GetAllCustomersAsync(BookStoreContext context)
        {
            return await context.Customer
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
    }
}
