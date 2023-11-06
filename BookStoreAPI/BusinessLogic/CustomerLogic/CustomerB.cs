using BookStoreData.Data;
using BookStoreAPI.Helpers.BaseBusinessLogic;
using BookStoreData.Models.Customers;
using BookStoreViewModels.ViewModels.Customers;
using BookStoreViewModels.ViewModels.Customers.Address;

namespace BookStoreAPI.BusinessLogic.CustomerLogic
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
