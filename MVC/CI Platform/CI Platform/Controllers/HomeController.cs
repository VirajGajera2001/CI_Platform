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
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Reset(string email, string token)
        {
            var passwordReset = _cidbcontext.PasswordResets.FirstOrDefault(pr => pr.Email == email && pr.Token == token);
            if (passwordReset == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // Pass the email and token to the view for resetting the password
            var model = new Resetpass
            {
                Email = email,
                Token = token
            };
            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Reset(Resetpass respa)
        {
            // Find the user by email
            var user = _cidbcontext.Users.FirstOrDefault(u => u.Email == respa.Email);
            if (user == null)
            {
                return RedirectToAction("Forget", "Home");
            }

            // Find the password reset record by email and token
            var passwordReset = _cidbcontext.PasswordResets.FirstOrDefault(pr => pr.Email == respa.Email && pr.Token == respa.Token);
            if (passwordReset == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // Update the user's password
            user.Password = respa.Password;
            _cidbcontext.SaveChanges();

            // Remove the password reset record from the database



            return View("Login");
        }

        public IActionResult Landing()
        {
            List<Mission> missions = _cidbcontext.Missions.ToList();
            return View(missions);
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