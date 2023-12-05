using BookStoreAPI.Helpers;
using BookStoreAPI.Interfaces.Services;
using BookStoreData.Data;
using BookStoreData.Models.Accounts;
using BookStoreData.Models.Customers;
using BookStoreViewModels.ViewModels.Accounts.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStoreAPI.Services.Auth
{
    public class AuthService
        (UserManager<User> userManager, 
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration,
        IEmailSenderService emailSenderService,
        BookStoreContext context)
        : IAuthService
    {

        public async Task<(int, string)> Login(LoginForView model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return (0, "Invalid Email");
            if (!await userManager.CheckPasswordAsync(user, model.Password))
                return (0, "Invalid password");

            string token = await GenerateToken(user, model.Audience);
            return (1, token);
        }

        public async Task<(int, string)> Registration(RegisterForView model, string role)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return (0, "User already exists");

            var emailExists = await userManager.FindByEmailAsync(model.Email);
            if (emailExists != null)
                return (0, "Email already exists");

            Customer customer = new()
            {
                Name = model.Name,
                Surname = model.Surname,
                IsSubscribed = model.IsSubscribed
            };
            context.Customer.Add(customer);
            await DatabaseOperationHandler.TryToSaveChangesAsync(context);

            User user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                CustomerID = customer.Id,
                PhoneNumber = model.PhoneNumber,
            };
            var createUserResult = await userManager.CreateAsync(user, model.Password);
            if (!createUserResult.Succeeded)
                return (0, "User creation failed! Please check user details and try again.");
            if (createUserResult.Succeeded)
            {
                var tokenGenerated = await userManager.GenerateEmailConfirmationTokenAsync(user);
                var token = CodeTokenToURL(tokenGenerated);
                await emailSenderService.ConfirmEmailEmail(token, user);
            }
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));

            if (await roleManager.RoleExistsAsync(role))
                await userManager.AddToRoleAsync(user, role);

            return (1, "User created successfully!");
        }

        public async Task<(int, string)> CheckTokenValidity(string token)
        {
            try
            {
                if (token.IsNullOrEmpty())
                    return (0, "Empty");

                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

                if (jwtSecurityToken.ValidTo < DateTime.UtcNow.AddSeconds(10))
                    return (0, "NotValid");
                else
                    return (1, "Valid");
            }
            catch (Exception ex)
            {
                return (0, ex.Message);
            }
        }

        private async Task<string> GenerateToken(User user, string audience)
        {
            var userRoles = await userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, user.UserName),
               new Claim(ClaimTypes.NameIdentifier, user.Id),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTKey:Secret"]));
            var _TokenExpiryTimeInHour = Convert.ToInt64(configuration["JWTKey:TokenExpiryTimeInHour"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = configuration["JWTKey:ValidIssuer"],
                Audience = configuration["Audiences:" + audience],
                Expires = DateTime.UtcNow.AddHours(_TokenExpiryTimeInHour),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(authClaims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string CodeTokenToURL(string token)
        {
            byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
            return WebEncoders.Base64UrlEncode(tokenGeneratedBytes);
        }
        public string DecodeToken(string token)
        {
            var tokenDecodedBytes = WebEncoders.Base64UrlDecode(token);
            return Encoding.UTF8.GetString(tokenDecodedBytes);
        }
    }
}
