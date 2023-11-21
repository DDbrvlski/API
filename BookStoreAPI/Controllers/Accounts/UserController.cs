using BookStoreAPI.Helpers;
using BookStoreData.Data;
using BookStoreData.Models.Accounts;
using BookStoreViewModels.ViewModels.Accounts.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            user.Email = userData.Email;
            user.UserName = userData.Username;
            user.PhoneNumber = userData.PhoneNumber;
            customer.Name = userData.Name;
            customer.Surname = userData.Surname;

            //Sprawdzic czy username, phonenumber i email nie sa juz w bazie

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

            if (!changePasswordResult.Succeeded)
            {
                return BadRequest("Nie udało się zmienić hasła użytkownika");
            }

            return Ok(new { message = "Pomyślnie zmieniono hasło" });
        }

        [HttpPut("Edit-Address-Data")]
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

            var customer = await context.Customer.FirstOrDefaultAsync(x => x.IsActive && x.Id == user.CustomerID);

            if (customer == null)
            {
                return NotFound("Nie znaleziono danych klienta.");
            }

            //edytowac adresy email


            return await DatabaseOperationHandler.TryToSaveChangesAsync(context);
        }
    }
}
