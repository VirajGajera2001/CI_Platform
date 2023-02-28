using CI_Platform.DataModels;
using CI_Platform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CI_Platform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CIdbcontext _cidbcontext;

        public HomeController(ILogger<HomeController> logger,CIdbcontext cIdbcontext)
        {
            _logger = logger;
            _cidbcontext = cIdbcontext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(User user)
        {
                var userexists = _cidbcontext.Users.FirstOrDefault(u => u.Email == user.Email);
                if (userexists != null) {
                    ModelState.AddModelError("Email", "User with this email is already exists");
                    return View(user);
                }
                else
                {
                    _cidbcontext.Users.Add(user);
                    _cidbcontext.SaveChanges();
                    return RedirectToAction("Login");
                }
        }

        public IActionResult Forget()
        {
            return View();
        }
        public IActionResult Reset()
        {
            return View();
        }
        public IActionResult Landing()
        {
            return View();
        }
        public IActionResult NoMission()
        {
            return View();
        }
        public IActionResult RelatedMission()
        {
            return View();
        }
        public IActionResult Volunteering_Mission()
        {
            return View();
        }
        public IActionResult Story_Listing()
        {
            return View();
        }
        public IActionResult Share_Story()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}