using BookStoreAPI.Interfaces;
using BookStoreAPI.Services.Email;
using BookStoreData.Models.Accounts;
using BookStoreViewModels.ViewModels.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStoreAPI.Controllers.Accounts
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(UserManager<User> userManager) : ControllerBase
    {

        [HttpGet("info")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> GetUserInfo()
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

            var userInfo = new
            {
                UserName = user.UserName,
                Email = user.Email,
            };

            return Ok(userInfo);
        }
    }
}
