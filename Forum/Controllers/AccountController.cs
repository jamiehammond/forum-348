using Forum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
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

        // Register get method
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Register post method
        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> RegisterAsync(RegisterViewModel vm)
        {
            // If entered information is valid:
            if (ModelState.IsValid)
            {

                // Create new user
                var user = new ForumUser { UserName = vm.Username };
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
    }
}