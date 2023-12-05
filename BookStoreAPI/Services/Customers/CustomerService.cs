using BookStoreAPI.Helpers;
using BookStoreAPI.Interfaces.Services;
using BookStoreData.Data;
using BookStoreData.Models.Accounts;
using BookStoreData.Models.Customers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Services.Customers
{
    public class CustomerService(UserManager<User> userManager, IUserService userService, BookStoreContext context) : ICustomerService
    {
        public async Task<Customer> GetCustomerByEmail(string email)
        {
            var customerId = context.User.Where(x => x.Email == email && x.IsActive).FirstOrDefaultAsync().Result.CustomerID;
            return await context.Customer.FirstOrDefaultAsync(x => x.Id == customerId);
        }

        public async Task<IActionResult> SetCustomerNewsletterSubscription(Customer customer)
        {
            if (customer.IsSubscribed)
            {
                return new BadRequestObjectResult("E-mail jest już zasubskrybowany do newslettera.");
            }
            else
            {
                customer.IsSubscribed = true;
                await DatabaseOperationHandler.TryToSaveChangesAsync(context);
                return new OkObjectResult(new { Message = "E-mail został pomyślnie zasubskrybowany do newslettera." });
            }
        }
    }
}
