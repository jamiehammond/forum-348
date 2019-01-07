using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Forum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Forum.Controllers
{
    [Authorize]
    public class MemberHomeController : Controller
    {

        private readonly UserManager<ForumUser> _userManager;

        public MemberHomeController(UserManager<ForumUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {

            var claims = User.Claims;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
