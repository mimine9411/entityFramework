using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

public class AccountController : Controller
    {

    private readonly AppContext _context;

    public AccountController(AppContext context)
    {
        _context = context;
    }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == username);
            if (user != null && VerifyPassword(user.Password, password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "User"); // Redirect to a protected page
            }

            // If authentication fails, display the login page with an error
            ModelState.AddModelError(string.Empty, "Invalid username or password");
            ViewBag.LastUsername = username;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public bool VerifyPassword(string hashedPasswordWithSalt, string enteredPassword)
        {
            var parts = hashedPasswordWithSalt.Split(':');
            var salt = Convert.FromBase64String(parts[0]);
            var hashedPassword = parts[1];

            // Hash the entered password with the stored salt
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: enteredPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            // Compare the hashed password with the stored hash
            return hashedPassword == hashed;
        }
    }
