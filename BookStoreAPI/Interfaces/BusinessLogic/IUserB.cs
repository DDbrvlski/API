using BookStoreViewModels.ViewModels.Accounts.User;
using BookStoreViewModels.ViewModels.Customers.Address;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Interfaces.BusinessLogic
{
    public interface IUserB
    {
        Task<ActionResult<UserDataForView>> GetUserDataAsync();
        Task<ActionResult<IEnumerable<AddressDetailsForView>>> GetUserAddressDataAsync();
        Task<IActionResult> DeactivateUserAsync();
        Task<IActionResult> EditUserDataAsync(UserDataForView userData);
        Task<IActionResult> EditUserPasswordAsync(UserChangePasswordForView userData);
        Task<IActionResult> EditUserAddressDataAsync(UserAddressForView userData);
    }
}
