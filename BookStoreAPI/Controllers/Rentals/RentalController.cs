using BookStoreAPI.Helpers;
using BookStoreAPI.Helpers.BaseController;
using BookStoreAPI.Interfaces.Services;
using BookStoreAPI.Services.Customers;
using BookStoreAPI.Services.Users;
using BookStoreData.Data;
using BookStoreData.Models.Accounts;
using BookStoreData.Models.Rental;
using BookStoreViewModels.ViewModels.Rentals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BookStoreAPI.Controllers.Rentals
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController(BookStoreContext context, IUserService userService, ICustomerService customerService) : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = UserRoles.User)]
        [Route("New-Rental")]
        public async Task<IActionResult> AddNewRental(RentalPostForView newRental)
        {
            var user = await userService.GetUserByToken();

            if (user == null)
            {
                return new NotFoundObjectResult("Nie można znaleźć użytkownika.");
            }

            var customer = await customerService.GetCustomerByUser(user);

            if (customer == null)
            {
                return new NotFoundObjectResult("Nie znaleziono danych klienta.");
            }

            var days = await context.RentalType.Where(x => x.IsActive && x.Id == newRental.RentalTypeID).Select(x => x.Days).FirstOrDefaultAsync();

            Rental rental = new Rental();
            rental.CopyProperties(newRental);
            rental.CustomerID = customer.Id;
            rental.RentalStatusID = 1;
            rental.EndDate = rental.StartDate.AddDays(days);

            context.Rental.Add(rental);

            return await DatabaseOperationHandler.TryToSaveChangesAsync(context);
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.User)]
        [Route("Rented-Ebooks")]
        public async Task<ActionResult<IEnumerable<RentalForView>>> GetRentedEbooks()
        {
            var user = await userService.GetUserByToken();

            if (user == null)
            {
                return new NotFoundObjectResult("Nie można znaleźć użytkownika.");
            }

            var customer = await customerService.GetCustomerByUser(user);

            if (customer == null)
            {
                return new NotFoundObjectResult("Nie znaleziono danych klienta.");
            }

            var rentedEbook = await context.Rental
                .Include(x => x.BookItem)
                    .ThenInclude(x => x.Book)
                    .ThenInclude(x => x.BookImages)
                    .ThenInclude(x => x.Image)
                .Include(x => x.BookItem)
                    .ThenInclude(x => x.FileFormat)
                .Where(x => x.IsActive && x.CustomerID == customer.Id)
                .ToListAsync();

            List<RentalForView> rentedEbooks = new List<RentalForView>();
            
            foreach (var item in rentedEbook)
            {
                rentedEbooks.Add(new RentalForView()
                {
                    Id = item.Id,
                    BookItemId = item.BookItemID,
                    BookTitle = item.BookItem.Book.Title,
                    ExpirationDate = item.EndDate,
                    FileFormatName = item.BookItem.FileFormat.Name,
                    ImageURL = item.BookItem.Book.BookImages.FirstOrDefault(y => y.IsActive && y.Image.Position == 1).Image.ImageURL,
                });
            }

            return rentedEbooks;
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.User)]
        [Route("Purchased-Ebooks")]
        public async Task<ActionResult<IEnumerable<RentalForView>>> GetPurchasedEbooks()
        {
            var user = await userService.GetUserByToken();

            if (user == null)
            {
                return new NotFoundObjectResult("Nie można znaleźć użytkownika.");
            }

            var customer = await customerService.GetCustomerByUser(user);

            if (customer == null)
            {
                return new NotFoundObjectResult("Nie znaleziono danych klienta.");
            }

            var rentedEbook = await context.Rental
                .Include(x => x.BookItem)
                    .ThenInclude(x => x.Book)
                    .ThenInclude(x => x.BookImages)
                    .ThenInclude(x => x.Image)
                .Include(x => x.BookItem)
                    .ThenInclude(x => x.FileFormat)
                .Where(x => x.IsActive && x.CustomerID == customer.Id)
                .ToListAsync();

            List<RentalForView> rentedEbooks = new List<RentalForView>();

            foreach (var item in rentedEbook)
            {
                rentedEbooks.Add(new RentalForView()
                {
                    Id = item.Id,
                    BookItemId = item.BookItemID,
                    BookTitle = item.BookItem.Book.Title,
                    ExpirationDate = item.EndDate,
                    FileFormatName = item.BookItem.FileFormat.Name,
                    ImageURL = item.BookItem.Book.BookImages.FirstOrDefault(y => y.IsActive && y.Image.Position == 1).Image.ImageURL,
                });
            }

            return rentedEbooks;
        }
    }
}
