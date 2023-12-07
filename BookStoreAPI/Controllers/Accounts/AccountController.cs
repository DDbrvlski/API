using BookStoreAPI.Helpers;
using BookStoreAPI.Interfaces.Services;
using BookStoreData.Data;
using BookStoreData.Models.Accounts;
using BookStoreData.Models.Customers;
using BookStoreViewModels.ViewModels.Accounts.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
                return BadRequest(new { message = "Email został już potwierdzony" });
            }

            var decodedToken = authService.DecodeToken(token);

            // Potwierdź email użytkownika
            var result = await userManager.ConfirmEmailAsync(user, decodedToken);
            if (!result.Succeeded)
            {
                // Obsłuż błąd potwierdzania emaila
                return BadRequest(new { message = "Wystąpił błąd" });
            }

            return Ok(new { message = "Email został potwierdzony" });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordForView model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);

                await emailSenderService.ResetPasswordEmail(token, user);
            }

            return Ok(new { message = "Jeżeli istnieje konto z podanym emailem, wyślemy na niego wiadomość" });
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
                return Ok(new { message = "Hasło zostało zmienione" });
            }
            else
            {
                // Obsłuż błąd resetowania hasła
                return BadRequest(new { message = "Błąd zmiany hasła" });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("CheckTokenValidity")]
        public async Task<IActionResult> CheckTokenValidity(string token)
        {
            var (status, message) = await authService.CheckTokenValidity(token);
            if (status == 0)
            {
                return BadRequest(message);
            }
            return Ok(message);
        }

    }
}
