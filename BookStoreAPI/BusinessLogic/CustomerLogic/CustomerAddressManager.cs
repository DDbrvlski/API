using BookStoreAPI.Helpers;
using BookStoreData.Data;
using BookStoreData.Models.Customers;
using BookStoreData.Models.Products.Books;
using BookStoreViewModels.ViewModels.Customers.Address;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.BusinessLogic.CustomerLogic
{
    public class CustomerAddressManager
    {
        public static async Task UpdateAddresses(Customer customer, List<AddressPostForView?> addresses, BookStoreContext _context)
        {
            var existingAddressesIds = await _context.CustomerAddress
                .Where(x => x.CustomerID == customer.Id && x.IsActive == true)
                .Select(x => x.AddressID)
            .ToListAsync();

            var addressIds = addresses.Select(x => (int?)x.Id).ToList();

            var addressesToDeactivate = existingAddressesIds.Except(addressIds).ToList();
            var addressesToAdd = addresses.Where(x => x != null && !existingAddressesIds.Contains(x.Id)).ToList();

            if (addressesToDeactivate.Count() > 0)
            {
                await DatabaseOperationHandler.HandleDatabaseOperation(
                    async () => await DeactivateChosenAddresses(customer, addressesToDeactivate, _context),
                    "deaktywacji"
                );
            }

            if (addressesToAdd.Count() > 0)
            {
                await DatabaseOperationHandler.HandleDatabaseOperation(
                    async () => await AddNewAddresses(customer, addressesToAdd, _context),
                    "dodawania"
                );
            }
        }

        public static async Task AddNewAddresses(Customer customer, List<AddressPostForView?> addressesToAdd, BookStoreContext _context)
        {
            if (addressesToAdd?.Count > 0)
            {
                var newAddresses = addressesToAdd
                    .Where(address => address != null)
                    .Select(address => new Address
                    {

                    }.CopyProperties(address)).ToList();

                _context.Address.AddRange(newAddresses);

                await DatabaseOperationHandler.TryToSaveChangesAsync(_context);

                var customerAddresses = newAddresses
                    .Select(address => new CustomerAddress
                    {
                        CustomerID = customer.Id,
                        AddressID = address.Id
                    }).ToList();

                _context.CustomerAddress.AddRange(customerAddresses);

                await DatabaseOperationHandler.TryToSaveChangesAsync(_context);
            }
        }
        public static async Task DeactivateAllAddresses(Customer customer, BookStoreContext _context)
        {
            var addresses = await _context.CustomerAddress
                .Where(x => x.CustomerID == customer.Id && x.IsActive == true)
                .ToListAsync();

            foreach (var address in addresses)
            {
                address.IsActive = false;
            }

            await DatabaseOperationHandler.TryToSaveChangesAsync(_context);
        }
        public static async Task DeactivateChosenAddresses(Customer customer, List<int?> addressIdsToDeactivate, BookStoreContext _context)
        {
            var customerAddressesToDeactivate = await _context.CustomerAddress
                .Where(x => x.CustomerID == customer.Id && addressIdsToDeactivate.Contains(x.AddressID) && x.IsActive == true)
                .ToListAsync();

            foreach (var customerAddress in customerAddressesToDeactivate)
            {
                customerAddress.IsActive = false;
            }

            var addressesToDeactivate = await _context.Address
                .Where(x => addressIdsToDeactivate.Contains(x.Id) && x.IsActive == true)
                .ToListAsync();

            foreach (var address in addressesToDeactivate)
            {
                address.IsActive = false;
            }

            await DatabaseOperationHandler.TryToSaveChangesAsync(_context);
        }
    }
}
