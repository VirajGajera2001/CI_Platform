using CI_Platform.DataModels;
using CI_Platform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using cloudscribe.Pagination.Models;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Net.Mail;
using System.Net;
using NuGet.Common;
using Newtonsoft.Json.Linq;
using System.Text;

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
        [HttpGet]
        public IActionResult Landing()
        {
            var fname = HttpContext.Session.GetString("FName");
            if (fname == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                List<Mission> missions = _cidbcontext.Missions.ToList();
                List<City> cities = _cidbcontext.Cities.ToList();
                List<MissionViewModel> missionViewModels = new List<MissionViewModel>();
                List<CitydataModel> citydataModels = new List<CitydataModel>();
                List<Country> countries1 = _cidbcontext.Countries.ToList();
                List<CountrydataModel> countrydataModels = new List<CountrydataModel>();
                List<MissionTheme> missionThemes = _cidbcontext.MissionThemes.ToList();
                List<ThemedataModel> themedataModels = new List<ThemedataModel>();
                var userId = HttpContext.Session.GetString("UserId");
                foreach (var citie in cities)
                {
                    CitydataModel citydataModels1 = new CitydataModel();
                    var cityname = _cidbcontext.Cities.FirstOrDefault(c => c.CityId == citie.CityId);
                    citydataModels1.Name = cityname.Name;
                    citydataModels1.CityId = citie.CityId;
                    citydataModels.Add(citydataModels1);
                }
                foreach (var countries in countries1)
                {
                    CountrydataModel countrydataModels1 = new CountrydataModel();
                    var countryname = _cidbcontext.Countries.FirstOrDefault(co => co.CountryId == countries.CountryId);
                    countrydataModels1.CountryId = countryname.CountryId;
                    countrydataModels1.Name = countryname.Name;
                    countrydataModels.Add(countrydataModels1);
                }
                foreach (var themes in missionThemes)
                {
                    ThemedataModel themedataModels1 = new ThemedataModel();
                    var themename = _cidbcontext.MissionThemes.FirstOrDefault(t => t.MissionThemeId == themes.MissionThemeId);
                    themedataModels1.MissionThemeId = themename.MissionThemeId;
                    themedataModels1.Title = themename.Title;
                    themedataModels.Add(themedataModels1);
                }
                foreach (var mission in missions)
                {
                    MissionViewModel missionView = new MissionViewModel();
                    missionView.Availability = mission.Availability;
                    missionView.MissionId = mission.MissionId;
                    missionView.Title = mission.Title;
                    missionView.Description = mission.Description;
                    missionView.ShortDescription = mission.ShortDescription;
                    missionView.StartDate = mission.StartDate;
                    missionView.EndDate = mission.EndDate;
                    missionView.CountryId = mission.CountryId;
                    missionView.CityId = mission.CityId;
                    missionView.ThemeId = mission.ThemeId;
                    missionView.MissionType = mission.MissionType;
                    missionView.SeatsAvailable = mission.SeatsAvailable;
                    var rating1 = _cidbcontext.MissionRatings.Where(rt => rt.MissionId == mission.MissionId);
                    var rat1 = 0;
                    var sum = 0;
                    foreach (var rat in rating1)
                    {
                        sum = sum + int.Parse(rat.Rating);
                    }
                    if (rating1.Count() == 0)
                    {
                        rat1 = 0;
                        missionView.Rating = rat1;
                    }
                    else
                    {
                        rat1 = sum / rating1.Count();
                        missionView.Rating = rat1;
                    }
                    var isfav=_cidbcontext.FavouriteMissions.Where(fm=>fm.MissionId==mission.MissionId&&fm.UserId== int.Parse(userId)).ToList();
                    if(isfav.Count>0)
                    {
                        missionView.isFav = true;
                    }
                    else
                    {
                        missionView.isFav = false;
                    }

                    var city = _cidbcontext.Cities.FirstOrDefault(c => c.CityId == mission.CityId);
                    if (city != null)
                    {
                        missionView.City = city.Name;
                    }
                    var themes = _cidbcontext.MissionThemes.FirstOrDefault(t => t.MissionThemeId == mission.ThemeId);
                    if (themes != null)
                    {
                        missionView.Theme = themes.Title;
                    }
                    var country = _cidbcontext.Countries.FirstOrDefault(co => co.CountryId == mission.CountryId);
                    if (country != null)
                    {
                        missionView.Country = country.Name;
                    }
                    var media = _cidbcontext.MissionMedia.FirstOrDefault(mi => mi.MissionId == mission.MissionId);
                    if (media != null)
                    {
                        missionView.MediaPath = media.MediaPath;
                    }
                    var goalvalue=_cidbcontext.GoalMissions.FirstOrDefault(gm=>gm.MissionId== mission.MissionId);
                    if(goalvalue != null)
                    {
                        missionView.GoalValue = goalvalue.GoalValue;
                    }
                    missionViewModels.Add(missionView);
                }
                string jsonData = JsonSerializer.Serialize(missionViewModels);
                string jsonCity = JsonSerializer.Serialize(citydataModels);
                string jsonCountry = JsonSerializer.Serialize(countrydataModels);
                string jsontheme = JsonSerializer.Serialize(themedataModels);
                ViewBag.Themes = jsontheme;
                ViewBag.Country = jsonCountry;
                ViewBag.City = jsonCity;
                ViewBag.JsonData = jsonData;
                ViewBag.UserId = int.Parse(userId);
                ViewBag.MissionVM = missionViewModels;
                return View();
            }
            
        }
        public IActionResult NoMission()
        {
            return View();
        }
        public IActionResult RelatedMission(int MissionId)
        {

            return View();
        }
        public IActionResult Volunteering_Mission(int MissionId, long? UserId)
        {
            if (UserId!=null)
            {
                var missions = _cidbcontext.Missions.FirstOrDefault(m => m.MissionId == MissionId);
                var city = _cidbcontext.Cities.FirstOrDefault(c => c.CityId == missions.CityId);
                var theme = _cidbcontext.MissionThemes.FirstOrDefault(mt => mt.MissionThemeId == missions.ThemeId);
                var relatedmission = _cidbcontext.Missions.Where(t => t.MissionId != MissionId && t.ThemeId == missions.ThemeId);
                var recentvol = from u in _cidbcontext.Users join ma in _cidbcontext.MissionApplications on u.UserId equals ma.UserId where ma.MissionId == missions.MissionId select u;
                var rating1 = _cidbcontext.MissionRatings.Where(rt => rt.MissionId == missions.MissionId);
                var goalvalue=_cidbcontext.GoalMissions.FirstOrDefault(gm=>gm.MissionId==missions.MissionId);
                
                MissionViewModel missionViewModels = new MissionViewModel();
                List<User> users = _cidbcontext.Users.ToList();
                List<Mission> missionlist = _cidbcontext.Missions.ToList();
                List<Comment> comments = _cidbcontext.Comments.ToList();
                List<CommentModel> commentModels= new List<CommentModel>();
                foreach(var comms in comments)
                {
                    CommentModel commentModels1= new CommentModel();
                    var user = _cidbcontext.Users.FirstOrDefault(t => t.UserId == comms.UserId && comms.MissionId==MissionId);
                    commentModels1.FirstName = user.FirstName;
                    commentModels1.Avatar= user.Avatar;
                    commentModels1.CommentText = comms.CommentText;
                    commentModels.Add(commentModels1);
                }
                missionViewModels.Status = missions.Status;
                var favo = _cidbcontext.FavouriteMissions.Where(e => e.MissionId == missions.MissionId && e.UserId == UserId);
                if (favo.Count() > 0)
                {
                    missionViewModels.isFav = true;
                }
                else
                {
                    missionViewModels.isFav = false;
                }

                var rat1 = 0;
                var sum = 0;
                foreach (var rat in rating1)
                {
                    sum = sum + int.Parse(rat.Rating);
                }
                if (rating1.Count() == 0)
                {
                    rat1 = 0;
                }
                else
                {
                    rat1 = sum / rating1.Count();
                }
                ViewBag.RecentVolunteering = recentvol;

                ViewBag.userId = UserId;
                var prewrating = _cidbcontext.MissionRatings.FirstOrDefault(r => r.MissionId == missions.MissionId && r.UserId == UserId);
                if (prewrating != null)
                {
                    ViewBag.Prewrating = int.Parse(prewrating.Rating);
                    ViewBag.AvgRating = rat1;
                }
                else
                {
                    ViewBag.Prewrating = 0;
                }
                ViewBag.Missions = missions;
                ViewBag.relatedmission = relatedmission;
                ViewBag.MissionTheme = theme;
                ViewBag.City = city;
                ViewBag.AllUsers = users;
                ViewBag.GoalVal = goalvalue;
                ViewBag.ShowComm = commentModels;
                return View(missionViewModels);
            }
            else
            {
                var missions = _cidbcontext.Missions.FirstOrDefault(m => m.MissionId == MissionId);
                var city = _cidbcontext.Cities.FirstOrDefault(c => c.CityId == missions.CityId);
                var theme = _cidbcontext.MissionThemes.FirstOrDefault(mt => mt.MissionThemeId == missions.ThemeId);
                var relatedmission = _cidbcontext.Missions.Where(t => t.MissionId != MissionId && t.ThemeId == missions.ThemeId);
                var recentvol = from u in _cidbcontext.Users join ma in _cidbcontext.MissionApplications on u.UserId equals ma.UserId where ma.MissionId == missions.MissionId select u;
                var rating1 = _cidbcontext.MissionRatings.Where(rt => rt.MissionId == missions.MissionId);
                var userId = HttpContext.Session.GetString("UserId");
                var goalvalue = _cidbcontext.GoalMissions.FirstOrDefault(gm => gm.MissionId == missions.MissionId);
                MissionViewModel missionViewModels = new MissionViewModel();
                List<User> users = _cidbcontext.Users.ToList();
                List<Mission> missionlist = _cidbcontext.Missions.ToList();
                List<Comment> comments = _cidbcontext.Comments.ToList();
                List<CommentModel> commentModels = new List<CommentModel>();
                foreach (var comms in comments)
                {
                    CommentModel commentModels1 = new CommentModel();
                    var user = _cidbcontext.Users.FirstOrDefault(t => t.UserId == comms.UserId && comms.MissionId == MissionId);
                    if(user!= null)
                    {
                        commentModels1.FirstName = user.FirstName;
                        commentModels1.CommentText = comms.CommentText;
                        commentModels1.CreatedAt = comms.CreatedAt;
                        commentModels1.Avatar = user.Avatar;
                        commentModels.Add(commentModels1);
                    }
                    
                }
                missionViewModels.Status = missions.Status;
                var favo = _cidbcontext.FavouriteMissions.Where(e => e.MissionId == missions.MissionId && e.UserId == int.Parse(userId));
                if (favo.Count() > 0)
                {
                    missionViewModels.isFav = true;
                }
                else
                {
                    missionViewModels.isFav = false;
                }

                var rat1 = 0;
                var sum = 0;
                foreach (var rat in rating1)
                {
                    sum = sum + int.Parse(rat.Rating);
                }
                if (rating1.Count() == 0)
                {
                    rat1 = 0;
                }
                else
                {
                    rat1 = sum / rating1.Count();
                }
                ViewBag.RecentVolunteering = recentvol;

                ViewBag.userId = int.Parse(userId);
                var prewrating = _cidbcontext.MissionRatings.FirstOrDefault(r => r.MissionId == missions.MissionId && r.UserId == int.Parse(userId));
                if (prewrating != null)
                {
                    ViewBag.Prewrating = int.Parse(prewrating.Rating);
                    ViewBag.AvgRating = rat1;
                }
                else
                {
                    ViewBag.Prewrating = 0;
                }
                ViewBag.Missions = missions;
                ViewBag.relatedmission = relatedmission;
                ViewBag.MissionTheme = theme;
                ViewBag.City = city;
                ViewBag.AllUsers = users;
                ViewBag.GoalVal = goalvalue;
                ViewBag.ShowComm = commentModels;
                return View(missionViewModels);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Addrating(string rating,long Id,long missionId)
        {
            MissionRating ratingExists=await _cidbcontext.MissionRatings.FirstOrDefaultAsync(fm=>fm.UserId== Id &&fm.MissionId==missionId);
            if (ratingExists != null) { 
                ratingExists.Rating = rating;
                _cidbcontext.Update(ratingExists);
                _cidbcontext.SaveChanges();
                return Json(new { success = true, ratingExists, isRated = true });
            }
            else
            {
                var ratingele = new MissionRating();
                ratingele.Rating = rating;
                ratingele.UserId = Id;
                ratingele.MissionId= missionId;
                _cidbcontext.Add(ratingele);
                _cidbcontext.SaveChanges();
                return Json(new { success = true, ratingele, isRated = true });
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddFav(long missionId,long Id)
        {
            FavouriteMission favexists =  _cidbcontext.FavouriteMissions.FirstOrDefault(fm => fm.UserId == Id && fm.MissionId == missionId);
            if (favexists != null)
            {
                favexists.MissionId = missionId;
                favexists.UserId= Id;
                _cidbcontext.Remove(favexists);
                 await _cidbcontext.SaveChangesAsync();
                return Json(new { success = true, favexists, isLiked = true });
            }
            else
            {
                var favele = new FavouriteMission();
                favele.MissionId= missionId;
                favele.UserId= Id;
                _cidbcontext.Add(favele);
                await _cidbcontext.SaveChangesAsync();
                return Json(new { success = true,favele, isLiked = true });
            }
        }
        [HttpPost]
        public async Task<IActionResult> SendRec(long missionId, string[] ToMail) {
            foreach(var items in ToMail)
            {
                var uId = _cidbcontext.Users.FirstOrDefault(u => u.Email == items);
                var resetLink = "https://localhost:44345" + Url.Action("Volunteering_Mission", "Home", new { MissionId = missionId, UserId = uId.UserId });
                var fromAddress = new MailAddress("gajeravirajpareshbhai@gmail.com", "Sender Name");
                var toAddress = new MailAddress(items);
                var subject = "Message For Recommand Mission";
                var body = $"Hi,<br /><br />Please click on the following link to reset your password:<br /><br /><a href='{resetLink}'>{resetLink}</a>";
                var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                var smtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("gajeravirajpareshbhai@gmail.com", "drbwjzfrmubtveud"),
                    EnableSsl = true
                };
                smtpClient.Send(message);
            }
            return Json(new { success = true });
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(long missionId,long userId,string commentText)
        {
            Comment comment=_cidbcontext.Comments.FirstOrDefault(cm=>cm.MissionId==missionId &&cm.UserId==userId);
            if (comment == null)
            {
                var addcomm = new Comment();
                addcomm.MissionId = missionId;
                addcomm.UserId = userId;
                addcomm.CommentText = commentText;
                addcomm.CreatedAt = DateTime.Now;
                _cidbcontext.Add(addcomm);
                await _cidbcontext.SaveChangesAsync();
            }
            else
            {
                var addcomm = new Comment();
                addcomm.MissionId = missionId;
                addcomm.UserId = userId;
                addcomm.CommentText = commentText;
                addcomm.CreatedAt = DateTime.Now;
                _cidbcontext.Add(addcomm);
                await _cidbcontext.SaveChangesAsync();
            }
            return Json(new {success= true});
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