using BookStoreAPI.Interfaces;
using BookStoreData.Models.Accounts;
using BookStoreViewModels.ViewModels.Accounts;
using Microsoft.AspNetCore.Identity;
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
        IEmailSenderService emailSenderService)
        : IAuthService
    {

        public async Task<(int, string)> Login(LoginForView model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return (0, "Invalid username");
            if (!await userManager.CheckPasswordAsync(user, model.Password))
                return (0, "Invalid password");

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
            string token = GenerateToken(authClaims, model.Audience);
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

            User user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
            };
            var createUserResult = await userManager.CreateAsync(user, model.Password);
            if (!createUserResult.Succeeded)
                return (0, "User creation failed! Please check user details and try again.");
            if (createUserResult.Succeeded)
            {
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                emailSenderService.ConfirmEmailEmail(token, user);
            }
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));

            if (await roleManager.RoleExistsAsync(role))
                await userManager.AddToRoleAsync(user, role);

            return (1, "User created successfully!");
        }

        private string GenerateToken(IEnumerable<Claim> claims, string audience)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTKey:Secret"]));
            var _TokenExpiryTimeInHour = Convert.ToInt64(configuration["JWTKey:TokenExpiryTimeInHour"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = configuration["JWTKey:ValidIssuer"],
                Audience = configuration["Audiences:" + audience],
                Expires = DateTime.UtcNow.AddHours(_TokenExpiryTimeInHour),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
