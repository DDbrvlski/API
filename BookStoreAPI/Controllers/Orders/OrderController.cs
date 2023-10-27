using BookStoreAPI.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreAPI.Models.Customers;
using BookStoreAPI.Models.Orders;
using BookStoreAPI.ViewModels.Customers.Address;
using BookStoreAPI.ViewModels.Customers;
using BookStoreAPI.ViewModels.Orders;
using CustomerStoreAPI.Models.BusinessLogic.CustomerLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreAPI.Helpers;
using BookStoreAPI.ViewModels.Orders.Dictionaries;
using BookStoreAPI.ViewModels.Payments;
using BookStoreAPI.ViewModels.Shippings;
using BookStoreAPI.ViewModels.Payments.Dictionaries;
using BookStoreAPI.ViewModels.Shippings.Dictionaries;
using BookStoreAPI.Models.BusinessLogic.OrderLogic;

namespace BookStoreAPI.Controllers.Orders
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : CRUDController<Order, OrderPostForView, OrderForView, OrderDetailsForView>
    {
        public OrderController(BookStoreContext context) : base(context)
        {
        }

        protected override async Task<OrderDetailsForView?> GetCustomEntityByIdAsync(int id)
        {
            var element = await _context.Order
                .Include(x => x.OrderItems)
                .Include(x => x.Customer)
                .Include(x => x.OrderStatus)
                .Include(x => x.DeliveryMethod)
                .Include(x => x.Payment)
                    .ThenInclude(x => x.TransactionStatus)
                .Include(x => x.Payment)
                    .ThenInclude(x => x.PaymentMethod)
                .Include(x => x.Shipping)
                    .ThenInclude(x => x.ShippingStatus)
                .Include(x => x.Shipping)
                    .ThenInclude(x => x.Address)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);

            return new OrderDetailsForView
            {
                Id = element.Id,
                DeliveryMethod = new DeliveryMethodForView
                {
                }.CopyProperties(element.DeliveryMethod),
                OrderStatus = new OrderStatusForView
                {
                }.CopyProperties(element.OrderStatus),
                Payment = new PaymentDetailsForView
                {
                    PaymentMethod = new PaymentMethodForView
                    {
                    }.CopyProperties(element.Payment.PaymentMethod),
                    TransactionStatus = new TransactionStatusForView
                    {
                    }.CopyProperties(element.Payment.TransactionStatus)
                }.CopyProperties(element.Payment),
                Shipping = new ShippingDetailsForView
                {
                    ShippingAddress = new AddressDetailsForView
                    {
                    }.CopyProperties(element.Shipping.Address),
                    ShippingStatus = new ShippingStatusForView
                    {
                    }.CopyProperties(element.Shipping.ShippingStatus)
                }.CopyProperties(element.Shipping),
                Customer = new CustomerForOrderForView
                {
                }.CopyProperties(element.Customer),

            }.CopyProperties(element);
        }
        protected override async Task<ActionResult<IEnumerable<OrderForView>>> GetAllEntitiesCustomAsync()
        {
            return await _context.Order
                .Include(x => x.OrderStatus)
                .Where(x => x.IsActive == true)
                .Select(x => new OrderForView
                {
                    OrderStatusName = x.OrderStatus.Name

                }.CopyProperties(x))
                .ToListAsync();
        }
        protected override async Task<IActionResult> CreateEntityCustomAsync(OrderPostForView entity)
        {
            return await OrderB.ConvertEntityPostForViewAndSave<OrderB>(entity, _context);
        }
        protected override async Task UpdateEntityCustomAsync(Order oldEntity, OrderPostForView updatedEntity)
        {
            await OrderB.ConvertEntityPostForViewAndUpdate<OrderB>(oldEntity, updatedEntity, _context);
        }
        protected override async Task<IActionResult> DeleteEntityCustomAsync(Order entity)
        {
            return await OrderB.DeactivateEntityAndSave<OrderB>(entity, _context);
        }
    }
}
