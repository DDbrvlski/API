using BookStoreAPI.Helpers;
using BookStoreData.Data;
using BookStoreData.Models.Accounts;
using BookStoreData.Models.Customers;
using BookStoreViewModels.ViewModels.Accounts.User;
using BookStoreViewModels.ViewModels.Customers.Address;
using BookStoreViewModels.ViewModels.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace BookStoreAPI.Controllers.Accounts
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.User)]
    public class UserController(UserManager<User> userManager, BookStoreContext context) : ControllerBase
    {

        [HttpGet("Data")]
        public async Task<IActionResult> GetUserData()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest("Nie można znaleźć identyfikatora użytkownika.");
            }

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("Nie można znaleźć użytkownika o podanym identyfikatorze.");
            }
            
            var customer = await context.Customer.FirstOrDefaultAsync(x => x.IsActive && x.Id == user.CustomerID);

            if (customer == null)
            {
                return NotFound("Nie znaleziono danych klienta.");
            }

            UserDataForView userData = new UserDataForView()
            {
                Name = customer.Name,
                Surname = customer.Surname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Username = user.UserName
            };

            return Ok(userData);
        }

        [HttpGet("Data-Address")]
        public async Task<ActionResult<IEnumerable<AddressDetailsForView>>> GetUserDataAddress()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest("Nie można znaleźć identyfikatora użytkownika.");
            }

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("Nie można znaleźć użytkownika o podanym identyfikatorze.");
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

            return Ok(customerAddresses);
        }

        [HttpDelete("Deactivate")]
        public async Task<IActionResult> DeactivateUserAccount()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest("Nie można znaleźć identyfikatora użytkownika.");
            }

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("Nie można znaleźć użytkownika o podanym identyfikatorze.");
            }

            var customer = await context.Customer.FirstOrDefaultAsync(x => x.IsActive && x.Id == user.CustomerID);

            if (customer == null)
            {
                return NotFound("Nie znaleziono danych klienta.");
            }

            //Dodac deaktywacje wishlisty

            customer.IsSubscribed = false;
            customer.IsActive = false;
            user.IsActive = false;

            //Dodać deaktywacje adresów

            return await DatabaseOperationHandler.TryToSaveChangesAsync(context);
        }

        [HttpPut("Edit-Data")]
        public async Task<IActionResult> EditUserData([FromBody] UserDataForView userData)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest("Nie można znaleźć identyfikatora użytkownika.");
            }

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("Nie można znaleźć użytkownika o podanym identyfikatorze.");
            }

            var customer = await context.Customer.FirstOrDefaultAsync(x => x.IsActive && x.Id == user.CustomerID);

            if (customer == null)
            {
                return NotFound("Nie znaleziono danych klienta.");
            }

            var isEmailExists = await userManager.FindByEmailAsync(userData.Email);

            if (isEmailExists != null && user.Email != userData.Email)
            {
                return BadRequest("Podany email jest już zajęty.");
            }

            var isUserNameExists = await userManager.FindByNameAsync(userData.Username);

            if (isEmailExists != null && user.UserName != userData.Username)
            {
                return BadRequest("Podana nazwa użytkownika jest już zajęta.");
            }

            user.Email = userData.Email;
            user.UserName = userData.Username;
            user.PhoneNumber = userData.PhoneNumber;
            customer.Name = userData.Name;
            customer.Surname = userData.Surname;


            return await DatabaseOperationHandler.TryToSaveChangesAsync(context);
        }

        [HttpPut("Edit-Password")]
        public async Task<IActionResult> EditUserPassword([FromBody] UserChangePasswordForView userData)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest("Nie można znaleźć identyfikatora użytkownika.");
            }

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("Nie można znaleźć użytkownika o podanym identyfikatorze.");
            }

            var isCurrentPasswordValid = await userManager.CheckPasswordAsync(user, userData.OldPassword);

            if (!isCurrentPasswordValid)
            {
                return BadRequest("Aktualne hasło jest nieprawidłowe");
            }

            if (userData.NewPassword != userData.RepeatNewPassword)
            {
                return BadRequest("Nowe hasło i powtórzone nowe hasło nie są identyczne");
            }

            var changePasswordResult = await userManager.ChangePasswordAsync(user, userData.OldPassword, userData.NewPassword);
            user.SecurityStamp = Guid.NewGuid().ToString();

            await DatabaseOperationHandler.TryToSaveChangesAsync(context);

            if (!changePasswordResult.Succeeded)
            {
                return BadRequest("Nie udało się zmienić hasła użytkownika");
            }

            return Ok(new { message = "Pomyślnie zmieniono hasło" });
        }

        [HttpPost]
        [Route("Edit-Address-Data")]
        public async Task<IActionResult> EditUserAddressData([FromBody] UserAddressForView userData)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest("Nie można znaleźć identyfikatora użytkownika.");
            }

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("Nie można znaleźć użytkownika o podanym identyfikatorze.");
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
            }
            else
            {
                Address address = new Address();
                Address mailingAddress = new Address();

                if (userData.address.Position == 0)
                {
                    userData.address.Position = 1;
                }
                if (userData.mailingAddress.Position == 0)
                {
                    userData.mailingAddress.Position = 2;
                }

                address.CopyProperties(userData.address);
                mailingAddress.CopyProperties(userData.mailingAddress);

                context.Address.Add(address);
                context.Address.Add(mailingAddress);

                await DatabaseOperationHandler.TryToSaveChangesAsync(context);

                CustomerAddress customerAddress1 = new CustomerAddress();
                CustomerAddress customerAddress2 = new CustomerAddress();

                customerAddress1.AddressID = address.Id;
                customerAddress1.CustomerID = user.CustomerID;
                customerAddress2.AddressID = mailingAddress.Id;
                customerAddress2.CustomerID = user.CustomerID;

                context.CustomerAddress.Add(customerAddress1);
                context.CustomerAddress.Add(customerAddress2);
            }

            return await DatabaseOperationHandler.TryToSaveChangesAsync(context);
        }
    }
}
