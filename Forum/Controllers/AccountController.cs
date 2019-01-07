using Forum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Forum.Controllers
{
    // Controller for account functions such as registering, logging in and logging out
    public class AccountController : Controller
    {
        private readonly UserManager<ForumUser> _userManager;
        private readonly SignInManager<ForumUser> _signInManager;

        // Constructor for class = create new usermanager and signinmanager
        public AccountController(UserManager<ForumUser> userManager, SignInManager<ForumUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Logs the current user out
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // Login get method
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Login post method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {

            // If entered viemodel information is valid:
            if (ModelState.IsValid)
            {
                // Attempts to login the user with the entered username and password
                var result = await _signInManager.PasswordSignInAsync(vm.Email, vm.Password, false, false);

                // If login successful, redirects them to the homepage
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                // Else, returns an error and returns the login view
                ModelState.AddModelError("", "Invalid email or password.");
                return View(vm);
            }
            // Return login view if model is not valid
            return View(vm);
        }

        // Register get method
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Register post method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            // If entered viewmodel information is valid:
            if (ModelState.IsValid)
            {

                // Create new user
                var user = new ForumUser { UserName = vm.Email, Email = vm.Email };
                var result = await _userManager.CreateAsync(user, vm.Password);

                // If creation of user is successful, log them in and redirect to home page
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                // Else, list errors
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            // Return register view if model is not valid
            return View(vm);
        }

        // Default path for authorisation access denied, if the current user does not have the required role
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}