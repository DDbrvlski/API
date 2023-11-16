using BookStoreAPI.Interfaces;
using BookStoreData.Models;
using BookStoreData.Models.Accounts;
using BookStoreViewModels.ViewModels.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Accounts
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController
        (IAuthService authService, 
        ILogger<AccountController> logger, 
        UserManager<User> userManager,
        IEmailSenderService emailSenderService) 
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
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid payload");
                var (status, message) = await authService.Registration(model, UserRoles.User);
                if (status == 0)
                {
                    return BadRequest(message);
                }
                return CreatedAtAction(nameof(Register), model);

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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

            // Potwierdź email użytkownika
            var result = await userManager.ConfirmEmailAsync(user, token);
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
    }
}
