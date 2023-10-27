//using BookStoreAPI.Data;
//using BookStoreAPI.Models.Accounts;
//using BookStoreAPI.Models.Accounts.Dictionaries;
//using BookStoreAPI.ViewModels.Accounts;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;

//namespace BookStoreAPI.Controllers.Accounts
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AccountController : ControllerBase
//    {
//        private readonly BookStoreContext context;
//        private readonly UserManager<LoginDetails> userManager;
//        private readonly SignInManager<LoginDetails> signInManager;
//        private readonly RoleManager<Permission> roleManager;

//        public AccountController(
//            UserManager<LoginDetails> userManager,
//            SignInManager<LoginDetails> signInManager,
//            RoleManager<Permission> roleManager,
//            BookStoreContext context)
//        {
//            this.userManager = userManager;
//            this.signInManager = signInManager;
//            this.roleManager = roleManager;
//            this.context = context;
//        }

//        [HttpPost("register")]
//        public async Task<IActionResult> Register([FromBody] RegisterForView model)
//        {
//            if (ModelState.IsValid)
//            {
//                // Sprawdź, czy istnieje użytkownik o takim samym emailu
//                var existingUser = await userManager.FindByEmailAsync(model.Email);
//                if (existingUser != null)
//                {
//                    return BadRequest("Użytkownik o podanym emailu już istnieje.");
//                }

//                var loginDetails = new LoginDetails
//                {
//                    Email = model.Email,
//                    PasswordHash = model.Password,
//                    UserName = model.UserName,
//                };
//                var result = await userManager.CreateAsync(loginDetails, model.Password);


//                // Utwórz nowego użytkownika
//                var newUser = new User
//                {
//                    PermissionID = 1,
//                    LoginDetailsID = loginDetails.Id,
//                    AccountStatusID = 1
//                };

//                if (result.Succeeded)
//                {
//                    // Użytkownik został pomyślnie zarejestrowany
//                    return Ok("Rejestracja zakończona sukcesem.");
//                }
//                else
//                {
//                    // Rejestracja nie powiodła się, zwróć błędy walidacji lub inne błędy
//                    return BadRequest(result.Errors);
//                }
//            }

//            return BadRequest("Nieprawidłowe dane rejestracji.");
//        }

//        [HttpPost("login")]
//        public async Task<IActionResult> Login([FromBody] LoginForView model)
//        {
//            if (ModelState.IsValid)
//            {
//                // Sprawdź, czy użytkownik o podanym emailu istnieje
//                var user = await userManager.FindByEmailAsync(model.Email);

//                if (user != null)
//                {
//                    // Użytkownik istnieje, sprawdź hasło
//                    var result = await signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);


//                    if (result.Succeeded)
//                    {
//                        // Użytkownik zalogowany pomyślnie
//                        // Tutaj możesz generować token JWT lub użyć innych mechanizmów sesji, w zależności od Twoich wymagań
//                        return Ok("Zalogowano pomyślnie.");
//                    }
//                }

//                // Logowanie nie powiodło się
//                return BadRequest("Nieprawidłowe dane logowania.");
//            }

//            return BadRequest("Nieprawidłowe dane logowania.");
//        }

//    }
//}
