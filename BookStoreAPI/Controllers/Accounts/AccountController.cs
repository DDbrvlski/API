using BookStoreAPI.Helpers;
using BookStoreAPI.Interfaces;
using BookStoreData.Data;
using BookStoreData.Models;
using BookStoreData.Models.Accounts;
using BookStoreData.Models.Customers;
using BookStoreViewModels.ViewModels.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BookStoreAPI.Controllers.Accounts
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController
        (IAuthService authService, 
        ILogger<AccountController> logger, 
        UserManager<User> userManager,
        IEmailSenderService emailSenderService,
        BookStoreContext context) 
        : ControllerBase
    {

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginForView model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid payload");
                var (status, message) = await authService.Login(model);
                if (status == 0)
                    return BadRequest(message);
                return Ok(message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> Register(RegisterForView model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    if (!ModelState.IsValid)
                        return BadRequest("Invalid payload");
                    var (status, message) = await authService.Registration(model, UserRoles.User);
                    if (status == 0)
                    {
                        return BadRequest(message);
                    }
                    transaction.Commit();
                    return CreatedAtAction(nameof(Register), model);

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    logger.LogError(ex.Message);
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            // Sprawdź czy token jest prawidłowy
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                // Nieprawidłowy użytkownik
                return BadRequest(new { message = "Invalid user." });
            }
            if (user.EmailConfirmed)
            {
                return BadRequest(new { message = "Już potiwerdzony." });
            }
            
            var tokenDecoded = DecodeToken(token);

            // Potwierdź email użytkownika
            var result = await userManager.ConfirmEmailAsync(user, tokenDecoded);
            if (!result.Succeeded)
            {
                // Obsłuż błąd potwierdzania emaila
                return BadRequest(new { message = "Email confirmation failed." });
            }

            return Ok(new { message = "Email został potwierdzony ;)" });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordForView model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);

                await emailSenderService.ResetPasswordEmail(token, user);

                return Ok(new { message = "token: " + token + " id: " + user.Id });
            }

            return Ok(new { message = "If the email exists in our system, we will send a password reset link." });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordForView model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return BadRequest(new { message = "Invalid user." });
            }

            var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                return Ok(new { message = "Hasło zostało zmienione." });
            }
            else
            {
                // Obsłuż błąd resetowania hasła
                return BadRequest(new { message = "Błąd zmiany hasła." });
            }
        }

        [HttpPost]
        //[AllowAnonymous]
        [Route("CreateCustomerData")]
        public async Task<IActionResult> CreateCustomerData(string userId, [FromBody] CreateCustomerDataForView model)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest(new { message = "Invalid user." });
            }

            var customer = await context.Customer.FirstAsync(x => x.Id == user.CustomerID);
            customer.CopyProperties(model);

            Address address1 = new Address();
            Address address2 = new Address();

            address1.CopyProperties(model.Address);
            address2.CopyProperties(model.MailingAddress);

            context.Address.Add(address1);
            context.Address.Add(address2);

            await DatabaseOperationHandler.TryToSaveChangesAsync(context);

            CustomerAddress customerAddress1 = new CustomerAddress();
            CustomerAddress customerAddress2 = new CustomerAddress();

            customerAddress1.AddressID = address1.Id;
            customerAddress1.CustomerID = customer.Id;
            customerAddress2.AddressID = address2.Id;
            customerAddress2.CustomerID = customer.Id;

            await DatabaseOperationHandler.TryToSaveChangesAsync(context);

            return Ok();

            //var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
            //if (result.Succeeded)
            //{
            //    return Ok(new { message = "Hasło zostało zmienione." });
            //}
            //else
            //{
            //    // Obsłuż błąd resetowania hasła
            //    return BadRequest(new { message = "Błąd zmiany hasła." });
            //}
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("CheckTokenValidity")]
        public async Task<IActionResult> CheckTokenValidity(string token)
        {
            try
            {
                if(token.IsNullOrEmpty())
                    return BadRequest(new { message = "Empty" });

                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

                if (jwtSecurityToken.ValidTo < DateTime.UtcNow.AddSeconds(10))
                    return BadRequest(new { message = "NotValid" });
                else
                    return Ok(new { message = "Valid" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        private string DecodeToken(string token)
        {
            var tokenDecodedBytes = WebEncoders.Base64UrlDecode(token);
            return Encoding.UTF8.GetString(tokenDecodedBytes);
        }
    }
}
