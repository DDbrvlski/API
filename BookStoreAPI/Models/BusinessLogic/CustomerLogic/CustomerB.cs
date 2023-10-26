using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Helpers.BaseBusinessLogic;
using BookStoreAPI.Models.Customers;
using BookStoreAPI.ViewModels.Customers;
using BookStoreAPI.ViewModels.Customers.Address;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerStoreAPI.Models.BusinessLogic.CustomerLogic
{
    public class CustomerB : BaseBusinessLogic<Customer, CustomerPostForView>
    {
        protected override async Task ConvertListsToUpdate(Customer entity, CustomerPostForView entityWithData, BookStoreContext context)
        {
            context.SaveChanges();
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

    }
}
