using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.Customers;
using BookStoreAPI.ViewModels.Customers;
using BookStoreAPI.ViewModels.Customers.Address;
using Microsoft.AspNetCore.Mvc;

namespace CustomerStoreAPI.Models.BusinessLogic.CustomerLogic
{
    public class CustomerB
    {
        public static async Task<IActionResult> ConvertCustomerPostForViewAndSave(CustomerPostForView customerWithData, BookStoreContext _context)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    Customer newCustomer = new Customer();
                    newCustomer.CopyProperties(customerWithData);

                    _context.Customer.Add(newCustomer);
                    await _context.SaveChangesAsync();

                    await ConvertListsToUpdate(newCustomer, customerWithData, _context);

                    transaction.Commit();
                    return new OkResult();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new BadRequestObjectResult(ex.Message);
                }
            }
        }

        public static async Task<IActionResult> ConvertCustomerPostForViewAndUpdate(Customer oldEntity, CustomerPostForView updatedEntity, BookStoreContext _context)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    oldEntity.CopyProperties(updatedEntity);
                    await _context.SaveChangesAsync();

                    await ConvertListsToUpdate(oldEntity, updatedEntity, _context);

                    transaction.Commit();
                    return new OkResult();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new BadRequestObjectResult(ex.Message);
                }
            }
        }

        public static async Task<IActionResult> DeactivateCustomer(Customer customer, BookStoreContext _context)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    customer.IsActive = false;
                    await _context.SaveChangesAsync();

                    await DeactivateAllConnectedEntities(customer, _context);

                    transaction.Commit();
                    return new OkResult();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new BadRequestObjectResult(ex.Message);
                }
            }
        }

        private static async Task ConvertListsToUpdate(Customer customerToUpdate, CustomerPostForView customerWithData, BookStoreContext _context)
        {
            List<AddressPostForView> addresses = customerWithData.ListOfCustomerAdresses.ToList();

            await UpdateAllConnectedEntitiesLists(customerToUpdate, addresses, _context);
        }

        private static async Task UpdateAllConnectedEntitiesLists(Customer customer, List<AddressPostForView?> addresses, BookStoreContext _context)
        {
            await CustomerAddressManager.UpdateAddresses(customer, addresses, _context);
        }

        private static async Task DeactivateAllConnectedEntities(Customer customer, BookStoreContext _context)
        {
            await CustomerAddressManager.DeactivateAllAddresses(customer, _context);
        }
    }
}
