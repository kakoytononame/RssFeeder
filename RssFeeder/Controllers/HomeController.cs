
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace donor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult MainPage()
        {
            return View();
        }
        
    }
}