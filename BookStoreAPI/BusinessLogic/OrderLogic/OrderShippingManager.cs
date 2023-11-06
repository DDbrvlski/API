using BookStoreData.Data;
using BookStoreAPI.Helpers;
using BookStoreData.Models.Customers;
using BookStoreData.Models.Delivery;
using BookStoreData.Models.Orders;
using BookStoreViewModels.ViewModels.Shippings;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.BusinessLogic.OrderLogic
{
    public class OrderShippingManager
    {
        public static async Task UpdateShipping(Order order, ShippingPostForView shipping, BookStoreContext _context)
        {
            if (order.ShippingID == null)
            {
                await AddNewShipping(order, shipping, _context);
            }
            else
            {
                var shippingToUpdate = await _context.Shipping.FirstAsync(x => x.Id == order.ShippingID);
                var addressToUpdate = await _context.Address.FirstAsync(x => x.Id == shippingToUpdate.AddressID);

                addressToUpdate.CopyProperties(shipping.Address);
                shippingToUpdate.CopyProperties(shipping);
                shippingToUpdate.ShippingStatusID = shipping.ShippingStatus.Id;

                _context.SaveChanges();
            }
        }

        public static async Task AddNewShipping(Order order, ShippingPostForView shipping, BookStoreContext _context)
        {
            Address newAddress = new Address();
            newAddress.CopyProperties(shipping.Address);
            _context.Address.Add(newAddress);
            _context.SaveChanges();

            Shipping newShipping = new Shipping();
            newShipping.CopyProperties(shipping);
            newShipping.AddressID = newAddress.Id;
            newShipping.ShippingStatusID = shipping.ShippingStatus.Id;
            _context.Shipping.Add(newShipping);
            _context.SaveChanges();

            order.ShippingID = newShipping.Id;
        }

        public static async Task DeactivateShipping(Order order, BookStoreContext _context)
        {
            var shippingToDeactivate = await _context.Shipping.FirstAsync(x => x.Id == order.ShippingID);
            var addressToDeactivate = await _context.Address.FirstAsync(x => x.Id == shippingToDeactivate.AddressID);

            shippingToDeactivate.IsActive = false;
            addressToDeactivate.IsActive = false;

            _context.SaveChanges();
        }
    }
}
