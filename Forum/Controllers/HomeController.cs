using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Forum.Models;

namespace Forum.Controllers
{
    // Controller for the home page
    public class HomeController : Controller
    {
        // Returns the index view
        public IActionResult Index()
        {
            return View();
        }

        // Error handling
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
