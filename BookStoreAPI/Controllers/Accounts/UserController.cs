using BookStoreAPI.Helpers;
using BookStoreAPI.Interfaces.BusinessLogic;
using BookStoreAPI.Interfaces.Services;
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
    public class UserController
        (UserManager<User> userManager, 
        BookStoreContext context, 
        IUserB userB)
        : ControllerBase
    {

        [HttpGet("Data")]
        public async Task<ActionResult<UserDataForView>> GetUserData()
        {
            return await userB.GetUserDataAsync();
        }

        [HttpGet("Data-Address")]
        public async Task<ActionResult<IEnumerable<AddressDetailsForView>>> GetUserDataAddress()
        {
            return await userB.GetUserAddressDataAsync();
        }

        [HttpDelete("Deactivate")]
        public async Task<IActionResult> DeactivateUserAccount()
        {
            return await userB.DeactivateUserAsync();       
        }

        [HttpPut("Edit-Data")]
        public async Task<IActionResult> EditUserData([FromBody] UserDataForView userData)
        {
            return await userB.EditUserDataAsync(userData);
        }

        [HttpPut("Edit-Password")]
        public async Task<IActionResult> EditUserPassword([FromBody] UserChangePasswordForView userData)
        {
            return await userB.EditUserPasswordAsync(userData);
        }

        [HttpPost]
        [Route("Edit-Address-Data")]
        public async Task<IActionResult> EditUserAddressData([FromBody] UserAddressForView userData)
        {
            return await userB.EditUserAddressDataAsync(userData);
        }
    }
}
