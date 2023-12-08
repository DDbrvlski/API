using BookStoreAPI.BusinessLogic.CustomerLogic;
using BookStoreAPI.BusinessLogic.WishlistLogic;
using BookStoreAPI.Helpers;
using BookStoreAPI.Interfaces.BusinessLogic;
using BookStoreAPI.Interfaces.Services;
using BookStoreData.Data;
using BookStoreData.Models.Accounts;
using BookStoreData.Models.Customers;
using BookStoreViewModels.ViewModels.Accounts.User;
using BookStoreViewModels.ViewModels.Customers.Address;
using BookStoreViewModels.ViewModels.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace BookStoreAPI.BusinessLogic.AccountLogic
{
    public class UserB
        (BookStoreContext context, 
        ICustomerService customerService, 
        IUserService userService, 
        UserManager<User> userManager)
        : IUserB
    {
        public async Task<ActionResult<UserDataForView>> GetUserDataAsync()
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

            UserDataForView userData = new UserDataForView()
            {
                Name = customer.Name,
                Surname = customer.Surname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Username = user.UserName
            };

            return new OkObjectResult(userData);
        }

        public async Task<ActionResult<IEnumerable<AddressDetailsForView>>> GetUserAddressDataAsync()
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

            var customerAddresses = await context.CustomerAddress
                .Include(x => x.Address)
                    .ThenInclude(x => x.City)
                .Include(x => x.Address)
                    .ThenInclude(x => x.Country)
                .Where(x => x.CustomerID == user.CustomerID && x.IsActive)
                .OrderBy(x => x.Address.Position)
                .Select(x => new AddressDetailsForView()
                {
                    Id = (int)x.AddressID,
                    Position = x.Address.Position,
                    CityID = x.Address.CityID,
                    CityName = x.Address.City.Name,
                    CountryID = x.Address.CountryID,
                    CountryName = x.Address.Country.Name,
                    HouseNumber = x.Address.HouseNumber,
                    Postcode = x.Address.Postcode,
                    Street = x.Address.Street,
                    StreetNumber = x.Address.StreetNumber,
                })
                .ToListAsync();

            return new OkObjectResult(customerAddresses);
        }

        public async Task<IActionResult> DeactivateUserAsync()
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

            customer.IsSubscribed = false;
            customer.IsActive = false;
            user.IsActive = false;

            await WishlistB.DeactivateWishlistAsync(customer, context);
            await CustomerAddressManager.DeactivateAllAddresses(customer, context);

            return await DatabaseOperationHandler.TryToSaveChangesAsync(context);
        }

        public async Task<IActionResult> EditUserDataAsync(UserDataForView userData)
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

            var isEmailExists = await userService.GetUserByEmail(userData.Email);

            if (isEmailExists != null && user.Email != userData.Email)
            {
                return new NotFoundObjectResult("Podany email jest już zajęty.");
            }

            var isUserNameExists = await userService.GetUserByUsername(userData.Username);

            if (isUserNameExists != null && user.UserName != userData.Username)
            {
                return new NotFoundObjectResult("Podana nazwa użytkownika jest już zajęta.");
            }

            user.Email = userData.Email;
            user.UserName = userData.Username;
            user.PhoneNumber = userData.PhoneNumber;
            customer.Name = userData.Name;
            customer.Surname = userData.Surname;

            return await DatabaseOperationHandler.TryToSaveChangesAsync(context);
        }

        public async Task<IActionResult> EditUserPasswordAsync(UserChangePasswordForView userData)
        {
            var user = await userService.GetUserByToken();

            if (user == null)
            {
                return new NotFoundObjectResult("Nie można znaleźć użytkownika.");
            }

            var isCurrentPasswordValid = await userManager.CheckPasswordAsync(user, userData.OldPassword);

            if (!isCurrentPasswordValid)
            {
                return new NotFoundObjectResult("Aktualne hasło jest nieprawidłowe");
            }

            if (userData.NewPassword != userData.RepeatNewPassword)
            {
                return new NotFoundObjectResult("Nowe hasło i powtórzone nowe hasło nie są identyczne");
            }

            var changePasswordResult = await userManager.ChangePasswordAsync(user, userData.OldPassword, userData.NewPassword);
            user.SecurityStamp = Guid.NewGuid().ToString();

            await DatabaseOperationHandler.TryToSaveChangesAsync(context);

            if (!changePasswordResult.Succeeded)
            {
                return new NotFoundObjectResult("Nie udało się zmienić hasła użytkownika");
            }

            return new OkObjectResult(new { message = "Pomyślnie zmieniono hasło" });
        }

        public async Task<IActionResult> EditUserAddressDataAsync(UserAddressForView userData)
        {
            var user = await userService.GetUserByToken();

            if (user == null)
            {
                return new NotFoundObjectResult("Nie można znaleźć użytkownika.");
            }

            var customerAddresses = await context.CustomerAddress
                .Include(x => x.Address)
                    .ThenInclude(x => x.City)
                .Include(x => x.Address)
                    .ThenInclude(x => x.Country)
                .Where(x => x.CustomerID == user.CustomerID && x.IsActive)
                .OrderBy(x => x.Address.Position)
                .Select(x => new BaseAddressView()
                {
                    Id = (int)x.AddressID,
                    Position = x.Address.Position,
                    CityID = x.Address.CityID,
                    CountryID = x.Address.CountryID,
                    HouseNumber = x.Address.HouseNumber,
                    Postcode = x.Address.Postcode,
                    Street = x.Address.Street,
                    StreetNumber = x.Address.StreetNumber,
                })
                .ToListAsync();

            if (userData.mailingAddress == null)
            {
                userData.mailingAddress.CopyProperties(userData.address);
            }

            if (!customerAddresses.IsNullOrEmpty())
            {
                var address = customerAddresses.Find(x => x.Position == 1);
                var mailingAddress = customerAddresses.Find(x => x.Position == 2);

                address.CopyProperties(userData.address);
                mailingAddress.CopyProperties(userData.mailingAddress);

                return await DatabaseOperationHandler.TryToSaveChangesAsync(context);
            }
            else
            {
                var customer = await customerService.GetCustomerByUser(user);

                if (customer == null)
                {
                    return new NotFoundObjectResult("Nie znaleziono danych klienta.");
                }

                if (userData.address.Position == 0)
                {
                    userData.address.Position = 1;
                }
                if (userData.mailingAddress.Position == 0)
                {
                    userData.mailingAddress.Position = 2;
                }

                List<BaseAddressView> addressesToAdd = new List<BaseAddressView>();
                addressesToAdd.Add(userData.address);
                addressesToAdd.Add(userData.mailingAddress);

                await CustomerAddressManager.AddNewAddresses(customer, addressesToAdd, context);

                return new OkResult();
            }
        }
    }
}
