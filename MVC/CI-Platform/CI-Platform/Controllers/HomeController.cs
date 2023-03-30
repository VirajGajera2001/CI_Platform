﻿using CI_Platform.Entities.DataModels;
using CI_Platform.Models;
using CI_Platform.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using CI_Platform.Entities.Models;

namespace CI_Platform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRegister _objRegister;
        private readonly ILanding _objLanding;
        private readonly IVolunteer _objVolunteer;
        private readonly IStoryListing _objStoryListing;


        public HomeController(ILogger<HomeController> logger, IRegister objRegister, ILanding objLanding, IVolunteer objVolunteer, IStoryListing objStoryListing)
        {
            _logger = logger;
            _objRegister = objRegister;
            _objLanding = objLanding;
            _objVolunteer = objVolunteer;
            _objStoryListing = objStoryListing;
        }

        [HttpGet]
        public ActionResult Login()
        {
            //HttpContext.Session.Clear();
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _objRegister.loguser(model);
                if (user != null)
                {
                    HttpContext.Session.SetString("FName", user.FirstName);
                    HttpContext.Session.SetString("UserId", user.UserId.ToString());
                    HttpContext.Session.SetString("Avatar", user.Avatar);
                    return RedirectToAction("Landing", "Home");
                }
                else
                {
                    ModelState.AddModelError("Email", "User is already exists please go to register");
                    return View(model);
                }
            }
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
            var userexists = _objRegister.IsExists(user.Email);
            if (userexists != null)
            {
                ModelState.AddModelError("Email", "User with this email is already exists");
                return View(user);
            }
            else
            {
                _objRegister.AddData(user);
                return RedirectToAction("Login");
            }
        }
        [HttpGet]
        public IActionResult Forget()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Forget(ForgetModel model)
        {


            if (ModelState.IsValid)
            {
                var user = _objRegister.forget(model);
                if (user == null)
                {
                    ModelState.AddModelError("Email", "Email is not valid");
                    return View(model);
                }

                // Generate a password reset token for the user
                var token = Guid.NewGuid().ToString();

                // Store the token in the password resets table with the user's email
                var passwordReset = new PasswordReset
                {
                    Email = model.Email,
                    Token = token
                };

                int newpassres = _objRegister.passres(passwordReset);

                // Send an email with the password reset link to the user's email address
                var resetLink = Url.Action("Reset", "Home", new { email = model.Email, token }, Request.Scheme);
                // Send email to user with reset password link
                // ...
                var fromAddress = new MailAddress("gajeravirajpareshbhai@gmail.com", "Sender Name");
                var toAddress = new MailAddress(model.Email);
                var subject = "Password reset request";
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

                return RedirectToAction("Login", "Home");
            }

            return View();
        }



        [HttpGet]
        [AllowAnonymous]
        public ActionResult Reset(string email, string token)
        {
            var passwordReset = _objRegister.PassReset(email, token);
            if (passwordReset == null)
            {
                return RedirectToAction("Login", "Home");
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
            var user = _objRegister.Pass(respa);
            if (user == null)
            {
                return RedirectToAction("Forget", "Home");
            }

            // Find the password reset record by email and token
            var passwordReset = _objRegister.reset(respa);
            if (passwordReset == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // Update the user's password
            bool istrue = _objRegister.AddPass(respa);
            if (istrue)
            {
                return View("Login");
            }

            // Remove the password reset record from the database



            return View();
        }

        [HttpGet]
        public IActionResult Landing(int pg = 1)
        {
            var fname = HttpContext.Session.GetString("FName");
            if (fname == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                List<MissionViewModel> missionViewModels = new List<MissionViewModel>();
                List<Mission> missions = _objLanding.missions();
                List<City> cities = _objLanding.cities();
                List<CitydataModel> citydataModels = new List<CitydataModel>();
                List<Country> countries1 = _objLanding.countries1();
                List<CountrydataModel> countrydataModels = new List<CountrydataModel>();
                List<MissionTheme> missionThemes = _objLanding.missionThemes();
                List<ThemedataModel> themedataModels = new List<ThemedataModel>();
                List<User> users = _objVolunteer.users();
                var userId = HttpContext.Session.GetString("UserId");
                foreach (var citie in cities)
                {
                    CitydataModel citydataModels1 = new CitydataModel();
                    var cityname = _objLanding.cityname(citie);
                    citydataModels1.Name = cityname.Name;
                    citydataModels1.CityId = citie.CityId;
                    citydataModels.Add(citydataModels1);
                }
                foreach (var countries in countries1)
                {
                    CountrydataModel countrydataModels1 = new CountrydataModel();
                    var countryname = _objLanding.countries(countries);
                    countrydataModels1.CountryId = countryname.CountryId;
                    countrydataModels1.Name = countryname.Name;
                    countrydataModels.Add(countrydataModels1);
                }
                foreach (var themes in missionThemes)
                {
                    ThemedataModel themedataModels1 = new ThemedataModel();
                    var themename = _objLanding.missionthemes(themes);
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
                    var rating1 = _objLanding.missionratings(mission);
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
                    var isfav = _objLanding.favouritemissions(mission, int.Parse(userId));
                    if (isfav.Count() > 0)
                    {
                        missionView.isFav = true;
                    }
                    else
                    {
                        missionView.isFav = false;
                    }
                    var city = _objLanding.cityname(mission);
                    if (city != null)
                    {
                        missionView.City = city.Name;
                    }
                    var themes = _objLanding.missionthemes(mission);
                    if (themes != null)
                    {
                        missionView.Theme = themes.Title;
                    }
                    var country = _objLanding.countries(mission);
                    if (country != null)
                    {
                        missionView.Country = country.Name;
                    }

                    var media = _objLanding.missionmedia(mission);
                    if (media != null)
                    {
                        missionView.MediaPath = media.MediaPath;
                    }
                    var goalvalue = _objLanding.goalmissions(mission);
                    if (goalvalue != null)
                    {
                        missionView.GoalValue = goalvalue.GoalValue;
                    }
                    var isapplied = _objLanding.applied(int.Parse(userId), mission.MissionId);
                    if (isapplied != null)
                    {
                        missionView.isapplied = true;
                    }
                    else
                    {
                        missionView.isapplied = false;
                    }
                    missionViewModels.Add(missionView);

                }
                ViewBag.CityData = citydataModels;
                ViewBag.CountryData = countrydataModels;
                ViewBag.ThemeData = themedataModels;
                ViewBag.UserId = int.Parse(userId);
                const int pageSize = 6;
                if (pg < 1)
                    pg = 1;
                int recsCount = missionViewModels.Count();
                var pager = new Pager(recsCount, pg, pageSize);
                int recSkip = (pg - 1) * pageSize;
                var data = missionViewModels.Skip(recSkip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;
                ViewBag.Count = missionViewModels.Count();
                ViewBag.AllUsers = users;
                return View(data);
            }

        }
        [HttpPost]
        public IActionResult Landing(string Title, string[] ToCountry, string[] ToCity, string[] ToTheme, string sortValue, int pg = 1)
        {
            List<MissionViewModel> missionViewModels = new List<MissionViewModel>();
            List<Mission> missions = _objLanding.missions();
            List<User> users = _objVolunteer.users();
            var userId = HttpContext.Session.GetString("UserId");
            if (!string.IsNullOrEmpty(Title))
            {
                missions = missions.Where(ms => ms.Title.ToLower().StartsWith(Title)).ToList();
            }
            if (ToCountry.Count() > 0)
            {
                missions = missions.Where(ms => ToCountry.Contains(Convert.ToString(ms.CountryId))).ToList();
            }
            if (ToCity.Count() > 0)
            {
                missions = missions.Where(ms => ToCity.Contains(Convert.ToString(ms.CityId))).ToList();
            }
            if (ToTheme.Count() > 0)
            {
                foreach (var id in ToTheme)
                {
                    missions = missions.Where(ms => ToTheme.Contains(Convert.ToString(ms.ThemeId))).ToList();
                }
            }
            if (sortValue != null)
            {
                switch (sortValue)
                {
                    case "Newest":
                        missions = missions.OrderByDescending(ms => ms.StartDate).ToList();
                        break;
                    case "Oldest":
                        missions = missions.OrderBy(ms => ms.StartDate).ToList();
                        break;
                    case "Lowest":
                        missions = missions.OrderBy(ms => ms.SeatsAvailable).ToList();
                        break;
                    case "Highest":
                        missions = missions.OrderByDescending(ms => ms.SeatsAvailable).ToList();
                        break;
                    case "Favourite":
                        List<FavouriteMission> isfav = _objLanding.isfav(int.Parse(userId));
                        var joinmis = from mis in missions join isf in isfav on mis.MissionId equals isf.MissionId select mis;
                        missions = joinmis.OrderBy(jm => jm.MissionId).ToList();
                        break;
                    case "Deadline":
                        missions = missions.OrderByDescending(ms => ms.EndDate).ToList();
                        break;
                }
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
                var rating1 = _objLanding.missionratings(mission);
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
                var isfav = _objLanding.favouritemissions(mission, int.Parse(userId));
                if (isfav.Count() > 0)
                {
                    missionView.isFav = true;
                }
                else
                {
                    missionView.isFav = false;
                }

                var city = _objLanding.cityname(mission);
                if (city != null)
                {
                    missionView.City = city.Name;
                }
                var themes = _objLanding.missionthemes(mission);
                if (themes != null)
                {
                    missionView.Theme = themes.Title;
                }
                var country = _objLanding.countries(mission);
                if (country != null)
                {
                    missionView.Country = country.Name;
                }
                var media = _objLanding.missionmedia(mission);
                if (media != null)
                {
                    missionView.MediaPath = media.MediaPath;
                }
                var goalvalue = _objLanding.goalmissions(mission);
                if (goalvalue != null)
                {
                    missionView.GoalValue = goalvalue.GoalValue;
                }
                var isapplied = _objLanding.applied(int.Parse(userId), mission.MissionId);
                if (isapplied != null)
                {
                    missionView.isapplied = true;
                }
                else
                {
                    missionView.isapplied = false;
                }
                missionViewModels.Add(missionView);
            }
            const int pageSize = 6;
            if (pg < 1)
                pg = 1;
            int recsCount = missionViewModels.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = missionViewModels.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            ViewBag.UserId = int.Parse(userId);
            ViewBag.Count = missionViewModels.Count();
            ViewBag.AllUsers = users;
            return PartialView("MissionCard", data);
        }
        public IActionResult NoMission()
        {
            return View();
        }
        public IActionResult RelatedMission(int MissionId, long? UserId)
        {
            List<MissionViewModel> missionViewModel = new List<MissionViewModel>();
            var missions = _objVolunteer.missions(MissionId);
            var relatedmission = _objVolunteer.missions(MissionId, missions);
            if (UserId != null)
            {

            }
            else
            {
                var userId = HttpContext.Session.GetString("UserId");
                foreach (var mission in relatedmission)
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
                    var rating1 = _objLanding.missionratings(mission);
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
                    var isfav = _objLanding.favouritemissions(mission, int.Parse(userId));
                    if (isfav.Count() > 0)
                    {
                        missionView.isFav = true;
                    }
                    else
                    {
                        missionView.isFav = false;
                    }
                    var city = _objLanding.cityname(mission);
                    if (city != null)
                    {
                        missionView.City = city.Name;
                    }
                    var themes = _objLanding.missionthemes(mission);
                    if (themes != null)
                    {
                        missionView.Theme = themes.Title;
                    }
                    var country = _objLanding.countries(mission);
                    if (country != null)
                    {
                        missionView.Country = country.Name;
                    }

                    var media = _objLanding.missionmedia(mission);
                    if (media != null)
                    {
                        missionView.MediaPath = media.MediaPath;
                    }
                    var goalvalue = _objLanding.goalmissions(mission);
                    if (goalvalue != null)
                    {
                        missionView.GoalValue = goalvalue.GoalValue;
                    }
                    var isapplied = _objLanding.applied(int.Parse(userId), mission.MissionId);
                    if (isapplied != null)
                    {
                        missionView.isapplied = true;
                    }
                    else
                    {
                        missionView.isapplied = false;
                    }
                    missionViewModel.Add(missionView);
                }
            }
            ViewBag.Relatedmission = missionViewModel;
            return View();
        }
        public IActionResult Volunteering_Mission(int MissionId, long? UserId, int pg = 1)
        {
            List<RelatedMissionView> rmv = new List<RelatedMissionView>();
            if (UserId != null)
            {
                var missions = _objVolunteer.missions(MissionId);
                var city = _objVolunteer.cities(missions);
                var theme = _objVolunteer.missiontheme(missions);
                var relatedmission = _objVolunteer.missions(MissionId, missions);
                var docs = _objVolunteer.missiondocs(MissionId);
                List<User> users = _objVolunteer.users();
                List<MissionApplication> missionapplications = _objVolunteer.missionapp();
                var recentvol = (from u in users join ma in missionapplications on u.UserId equals ma.UserId where ma.MissionId == missions.MissionId select u).ToList();
                var rating1 = _objVolunteer.missionratings(missions);
                var goalvalue = _objVolunteer.goalmissions(missions);
                var isapplied = _objVolunteer.applied(MissionId, UserId);
                if (isapplied != null)
                {
                    ViewBag.IsApplied = true;
                }
                else
                {
                    ViewBag.IsApplied = false;
                }
                foreach (var mis in relatedmission)
                {
                    RelatedMissionView ms = new RelatedMissionView();
                    ms.Availability = mis.Availability;
                    ms.MissionId = mis.MissionId;
                    ms.Title = mis.Title;
                    ms.Description = mis.Description;
                    ms.ShortDescription = mis.ShortDescription;
                    ms.StartDate = mis.StartDate;
                    ms.EndDate = mis.EndDate;
                    ms.CountryId = mis.CountryId;
                    ms.CityId = mis.CityId;
                    ms.ThemeId = mis.ThemeId;
                    ms.MissionType = mis.MissionType;
                    ms.SeatsAvailable = mis.SeatsAvailable;
                    var ratings1 = _objLanding.missionratings(mis);
                    var rats1 = 0;
                    var sums = 0;
                    foreach (var rat in ratings1)
                    {
                        sums = sums + int.Parse(rat.Rating);
                    }
                    if (ratings1.Count() == 0)
                    {
                        rats1 = 0;
                        ms.Rating = rats1;
                    }
                    else
                    {
                        rats1 = sums / ratings1.Count();
                        ms.Rating = rats1;
                    }
                    var isfav = _objVolunteer.favouritemissions(mis, UserId);
                    if (isfav.Count() > 0)
                    {
                        ms.isFav = true;
                    }
                    else
                    {
                        ms.isFav = false;
                    }

                    var citys = _objLanding.cityname(mis);
                    if (citys != null)
                    {
                        ms.City = citys.Name;
                    }
                    var themes = _objLanding.missionthemes(mis);
                    if (themes != null)
                    {
                        ms.Theme = themes.Title;
                    }
                    var country = _objLanding.countries(mis);
                    if (country != null)
                    {
                        ms.Country = country.Name;
                    }
                    var media = _objLanding.missionmedia(mis);
                    if (media != null)
                    {
                        ms.MediaPath = media.MediaPath;
                    }
                    var goalvalues = _objLanding.goalmissions(mis);
                    if (goalvalues != null)
                    {
                        ms.GoalValue = goalvalues.GoalValue;
                    }
                    var isapplieds = _objVolunteer.applied(UserId, mis.MissionId);
                    if (isapplieds != null)
                    {
                        ms.isapplied = true;
                    }
                    else
                    {
                        ms.isapplied = false;
                    }
                    rmv.Add(ms);
                }
                MissionViewModel missionViewModels = new MissionViewModel();
                List<Mission> missionlist = _objVolunteer.missions();
                List<Comment> comments = _objVolunteer.comments();
                List<CommentModel> commentModels = new List<CommentModel>();
                foreach (var comms in comments)
                {
                    CommentModel commentModels1 = new CommentModel();
                    var user = users.FirstOrDefault(t => t.UserId == comms.UserId && comms.MissionId == MissionId);
                    if (user != null)
                    {
                        commentModels1.FirstName = user.FirstName;
                        commentModels1.Avatar = user.Avatar;
                        commentModels1.CommentText = comms.CommentText;
                        commentModels.Add(commentModels1);
                    }
                }
                missionViewModels.Status = missions.Status;

                var favo = _objVolunteer.favmissions(missions, UserId);
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


                ViewBag.userId = UserId;
                var prewrating = _objVolunteer.missionrating(missions, UserId);
                if (prewrating != null)
                {
                    ViewBag.Prewrating = int.Parse(prewrating.Rating);
                    ViewBag.AvgRating = rat1;
                }
                else
                {
                    ViewBag.Prewrating = 0;
                }
                if (rating1 != null)
                {
                    ViewBag.AvgRating = rat1;
                }
                else
                {
                    ViewBag.AvgRating = 0;
                }
                const int pageSize = 3;
                if (pg < 1)
                    pg = 1;
                int recsCount = recentvol.Count();
                var pager = new Pager(recsCount, pg, pageSize);
                int recSkip = (pg - 1) * pageSize;
                var data = recentvol.Skip(recSkip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;
                ViewBag.RecentVolunteering = data;
                ViewBag.Missions = missions;
                ViewBag.relatedmission = relatedmission;
                ViewBag.MissionTheme = theme;
                ViewBag.City = city;
                ViewBag.AllUsers = users;
                ViewBag.GoalVal = goalvalue;
                ViewBag.ShowComm = commentModels;
                ViewBag.missionId = MissionId;
                ViewBag.Docs = docs;
                return View(missionViewModels);
            }
            else
            {
                var missions = _objVolunteer.missions(MissionId);
                var city = _objVolunteer.cities(missions);
                var theme = _objVolunteer.missiontheme(missions);
                var relatedmission = _objVolunteer.missions(MissionId, missions);
                var docs = _objVolunteer.missiondocs(MissionId);
                List<User> users = _objVolunteer.users();
                List<MissionApplication> missionapplications = _objVolunteer.missionapp();
                var recentvol = (from u in users join ma in missionapplications on u.UserId equals ma.UserId where ma.MissionId == missions.MissionId select u).ToList();
                const int pageSize = 3;
                if (pg < 1)
                    pg = 1;
                int recsCount = recentvol.Count();
                var pager = new Pager(recsCount, pg, pageSize);
                int recSkip = (pg - 1) * pageSize;
                var data = recentvol.Skip(recSkip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;
                ViewBag.RecentVolunteering = data;


                var rating1 = _objVolunteer.missionratings(missions);
                var goalvalue = _objVolunteer.goalmissions(missions);
                MissionViewModel missionViewModels = new MissionViewModel();
                List<Mission> missionlist = _objVolunteer.missions();
                List<Comment> comments = _objVolunteer.comments();
                List<CommentModel> commentModels = new List<CommentModel>();
                var userId = HttpContext.Session.GetString("UserId");
                var isapplied = _objVolunteer.appliedmis(MissionId, int.Parse(userId));
                if (isapplied != null)
                {
                    ViewBag.IsApplied = true;
                }
                else
                {
                    ViewBag.IsApplied = false;
                }
                foreach (var mission in relatedmission)
                {
                    RelatedMissionView missionView = new RelatedMissionView();
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
                    var ratings1 = _objLanding.missionratings(mission);
                    var rats1 = 0;
                    var sums = 0;
                    foreach (var ratt in ratings1)
                    {
                        sums = sums + int.Parse(ratt.Rating);
                    }
                    if (ratings1.Count() == 0)
                    {
                        rats1 = 0;
                        missionView.Rating = rats1;
                    }
                    else
                    {
                        rats1 = sums / ratings1.Count();
                        missionView.Rating = rats1;
                    }
                    var isfav = _objLanding.favouritemissions(mission, int.Parse(userId));
                    if (isfav.Count() > 0)
                    {
                        missionView.isFav = true;
                    }
                    else
                    {
                        missionView.isFav = false;
                    }
                    var citys = _objLanding.cityname(mission);
                    if (citys != null)
                    {
                        missionView.City = citys.Name;
                    }
                    var themes = _objLanding.missionthemes(mission);
                    if (themes != null)
                    {
                        missionView.Theme = themes.Title;
                    }
                    var country = _objLanding.countries(mission);
                    if (country != null)
                    {
                        missionView.Country = country.Name;
                    }

                    var media = _objLanding.missionmedia(mission);
                    if (media != null)
                    {
                        missionView.MediaPath = media.MediaPath;
                    }
                    var goalvalues = _objLanding.goalmissions(mission);
                    if (goalvalues != null)
                    {
                        missionView.GoalValue = goalvalues.GoalValue;
                    }
                    var isapplieds = _objLanding.applied(int.Parse(userId), mission.MissionId);
                    if (isapplieds != null)
                    {
                        missionView.isapplied = true;
                    }
                    else
                    {
                        missionView.isapplied = false;
                    }
                    rmv.Add(missionView);
                }
                foreach (var comms in comments)
                {
                    CommentModel commentModels1 = new CommentModel();
                    var user = users.FirstOrDefault(t => t.UserId == comms.UserId && comms.MissionId == MissionId);
                    if (user != null)
                    {
                        commentModels1.FirstName = user.FirstName;
                        commentModels1.CommentText = comms.CommentText;
                        commentModels1.CreatedAt = comms.CreatedAt;
                        commentModels1.Avatar = user.Avatar;
                        commentModels.Add(commentModels1);
                    }

                }
                missionViewModels.Status = missions.Status;
                var favo = _objVolunteer.favmission(missions, int.Parse(userId));
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


                ViewBag.userId = int.Parse(userId);
                var prewrating = _objVolunteer.missionrating(missions, int.Parse(userId));
                if (prewrating != null)
                {
                    ViewBag.Prewrating = int.Parse(prewrating.Rating);
                }
                else
                {
                    ViewBag.Prewrating = 0;
                }
                if (rating1 != null)
                {
                    ViewBag.AvgRating = rat1;
                }
                else
                {
                    ViewBag.AvgRating = 0;
                }

                ViewBag.Missions = missions;
                ViewBag.relatedmission = rmv;
                ViewBag.MissionTheme = theme;
                ViewBag.City = city;
                ViewBag.AllUsers = users;
                ViewBag.GoalVal = goalvalue;
                ViewBag.ShowComm = commentModels;
                ViewBag.missionId = MissionId;
                ViewBag.Docs = docs;
                return View(missionViewModels);
            }
        }
        [HttpPost]
        public IActionResult RecentVol(int MissionId, long? UserId, int pg = 1)
        {
            var missions = _objVolunteer.missions(MissionId);
            List<MissionApplication> missionapplications = _objVolunteer.missionapp();
            List<User> users = _objVolunteer.users();
            
                var recentvol = (from u in users join ma in missionapplications on u.UserId equals ma.UserId where ma.MissionId == missions.MissionId select u).ToList();
                const int pageSize = 3;
                if (pg < 1)
                    pg = 1;
                int recsCount = recentvol.Count();
                var pager = new Pager(recsCount, pg, pageSize);
                int recSkip = (pg - 1) * pageSize;
                var data = recentvol.Skip(recSkip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;
            ViewBag.RecentVolunteering = data;
            
            return PartialView("RecentVol");
        }

        [HttpPost]
        public IActionResult MissionApply(int MissionId)
        {
            var userId = HttpContext.Session.GetString("UserId");
            var apply = _objVolunteer.applyMission(MissionId, int.Parse(userId));
            if (apply == true)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Addrating(string rating, long Id, long missionId)
        {
            //MissionRating ratingExists = await _cidbcontext.MissionRatings.FirstOrDefaultAsync(fm => fm.UserId == Id && fm.MissionId == missionId);
            MissionRating ratingExists = _objVolunteer.ms(Id,missionId,rating);
            if (ratingExists != null)
            {
                
                return Json(new { success = true, ratingExists, isRated = true });
            }
            else
            {
                MissionRating ratingele = _objVolunteer.ms(Id, missionId, rating);
                return Json(new { success = true, ratingele, isRated = true });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddFav(long missionId, long Id)
        {
            FavouriteMission favexists = _objVolunteer.favms(missionId, Id);
            if (favexists != null)
            {
                return Json(new { success = true, favexists, isLiked = true });
            }
            else
            {
                FavouriteMission favele= _objVolunteer.favms(missionId, Id);
                return Json(new { success = true, favele, isLiked = true });
            }
        }
        [HttpPost]
        public async Task<IActionResult> SendRec(long missionId, string[] ToMail)
        {
            foreach (var items in ToMail)
            {
                List<User> users = _objVolunteer.users();
                var uId = users.FirstOrDefault(u => u.Email == items);
                var resetLink = "https://localhost:44390" + Url.Action("Volunteering_Mission", "Home", new { MissionId = missionId, UserId = uId.UserId });
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
        public async Task<IActionResult> AddComment(long missionId, long userId, string commentText)
        {
            
            Comment comment=_objVolunteer.comments(missionId,userId,commentText);
            if (comment == null)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = true });
            }
            
        }
        [HttpGet]
        public IActionResult Story_Listing(int pg = 1)
        {
            List<StoryViewModel> storyViewModel = new List<StoryViewModel>();
            List<Story> story = _objStoryListing.stories();
            List<City> cities = _objStoryListing.cities();
            List<CitydataModel> citydataModels = new List<CitydataModel>();
            List<Country> countries1 = _objStoryListing.countries1();
            List<CountrydataModel> countrydataModels = new List<CountrydataModel>();
            List<MissionTheme> missionThemes = _objStoryListing.missionThemes();
            List<ThemedataModel> themedataModels = new List<ThemedataModel>();
            foreach (var citie in cities)
            {
                CitydataModel citydataModels1 = new CitydataModel();
                var cityname = cities.FirstOrDefault(c => c.CityId == citie.CityId);
                citydataModels1.Name = cityname.Name;
                citydataModels1.CityId = citie.CityId;
                citydataModels.Add(citydataModels1);
            }
            foreach (var countries in countries1)
            {
                CountrydataModel countrydataModels1 = new CountrydataModel();
                var countryname = countries1.FirstOrDefault(co => co.CountryId == countries.CountryId);
                countrydataModels1.CountryId = countryname.CountryId;
                countrydataModels1.Name = countryname.Name;
                countrydataModels.Add(countrydataModels1);
            }
            foreach (var themes in missionThemes)
            {
                ThemedataModel themedataModels1 = new ThemedataModel();
                var themename = missionThemes.FirstOrDefault(t => t.MissionThemeId == themes.MissionThemeId);
                themedataModels1.MissionThemeId = themename.MissionThemeId;
                themedataModels1.Title = themename.Title;
                themedataModels.Add(themedataModels1);
            }
            foreach (var stories in story)
            {
                StoryViewModel storyview = new StoryViewModel();
                storyview.StoryId = stories.StoryId;
                storyview.UserId = stories.UserId;
                storyview.Title = stories.Title;
                storyview.MissionId = stories.MissionId;
                storyview.Status= stories.Status;
                storyview.Description = stories.Description;
                storyview.StoryDescription = stories.StoryDescription;
                var storymedia = _objStoryListing.storymedia(stories);
                if (storymedia != null)
                {
                    storyview.Path = storymedia.Path;
                }
                var storyusername = _objStoryListing.users(stories);
                if (storyusername != null)
                {
                    storyview.FirstName = storyusername.FirstName;
                    storyview.LastName = storyusername.LastName;
                    storyview.Avatar = storyusername.Avatar;
                }
                var storyrelatedmis = _objStoryListing.missions(stories);
                var storytheme = missionThemes.FirstOrDefault(mt => mt.MissionThemeId == storyrelatedmis.ThemeId);
                if (storytheme != null)
                {
                    storyview.ThemeTitle = storytheme.Title;
                }
                storyViewModel.Add(storyview);
            }
            const int pageSize = 6;
            if (pg < 1)
                pg = 1;
            int recsCount = storyViewModel.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = storyViewModel.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            ViewBag.CityData = citydataModels;
            ViewBag.CountryData = countrydataModels;
            ViewBag.ThemeData = themedataModels;
            return View(data);
        }
        [HttpPost]
        public IActionResult Story_Listing(string StorySearch, string[] ToCountry, string[] ToCity, string[] ToTheme, int pg = 1)
        {
            List<StoryViewModel> storyViewModel = new List<StoryViewModel>();
            List<Story> story = _objStoryListing.stories();
            List<Mission> mission = _objStoryListing.missions();
            List<MissionTheme> missionThemes = _objStoryListing.missionThemes();
            var userId = HttpContext.Session.GetString("UserId");
            if (!string.IsNullOrEmpty(StorySearch))
            {
                mission = mission.Where(ms => ms.Title.ToLower().StartsWith(StorySearch)).ToList();
            }
            if (ToCountry.Count() > 0)
            {
                mission = mission.Where(ms => ToCountry.Contains(Convert.ToString(ms.CountryId))).ToList();

            }
            if (ToCity.Count() > 0)
            {
                mission = mission.Where(ms => ToCity.Contains(Convert.ToString(ms.CityId))).ToList();
            }
            if (ToTheme.Count() > 0)
            {
                mission = mission.Where(ms => ToTheme.Contains(Convert.ToString(ms.ThemeId))).ToList();
            }

            story = (from st in story join ms in mission on st.MissionId equals ms.MissionId select st).ToList();
            foreach (var stories in story)
            {
                StoryViewModel storyview = new StoryViewModel();
                storyview.StoryId = stories.StoryId;
                storyview.UserId = stories.UserId;
                storyview.Title = stories.Title;
                storyview.MissionId = stories.MissionId;
                storyview.Description = stories.Description;
                storyview.StoryDescription = stories.StoryDescription;
                var storymedia = _objStoryListing.storymedia(stories);
                if (storymedia != null)
                {
                    storyview.Path = storymedia.Path;
                }
                var storyusername = _objStoryListing.users(stories);
                if (storyusername != null)
                {
                    storyview.FirstName = storyusername.FirstName;
                    storyview.LastName = storyusername.LastName;
                    storyview.Avatar = storyusername.Avatar;
                }
                var storyrelatedmis = _objStoryListing.missions(stories);
                var storytheme = missionThemes.FirstOrDefault(mt => mt.MissionThemeId == storyrelatedmis.ThemeId);
                if (storytheme != null)
                {
                    storyview.ThemeTitle = storytheme.Title;
                }
                storyViewModel.Add(storyview);
            }
            const int pageSize = 6;
            if (pg < 1)
                pg = 1;
            int recsCount = storyViewModel.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = storyViewModel.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            return PartialView("StoryCard", data);
        }
        public IActionResult Story_Detail(int StoryId, int UserId)
        {
            StoryDetailModel storydetailmodel= new StoryDetailModel();
            var storydetail = _objStoryListing.stories(StoryId);
            var storyuser = _objStoryListing.users(UserId);
            List<User> users = _objStoryListing.users();
            if (storydetail != null)
            {
                storydetailmodel.story = storydetail;
            }
            if (storyuser != null)
            {
                storydetailmodel.storyuser = storyuser;
            }
            storydetailmodel.Allusers = users;
            return View(storydetailmodel);
        }
        [HttpGet]
        public IActionResult Share_Story()
        {
            var userId = HttpContext.Session.GetString("UserId");
            List<Mission> mission = _objStoryListing.missions(int.Parse(userId));
                ViewBag.missions = mission;
            return View();
        }
        [HttpPost]
        
        public IActionResult Share_Storys(string[] Image,int MissionId,string Title,DateTime Date,string Description,int UserId,string Value)
        {
            var storyid = _objStoryListing.story(Image, MissionId, Title, Date, Description, UserId,Value);
            if (storyid!=null)
            {
                return Json(new { success = true,storyid});
            }
            else
            {
                return Json(new { success = false });
            }
        }
        [HttpPost]
        public IActionResult StoryEdit(int MissionId, int UserId)
        {
            Story story = _objStoryListing.searchstory(MissionId, UserId);
           
            if (story != null && story.Status=="DRAFT")
            {
                List<StoryMedium> media = _objStoryListing.searchmedia(story.StoryId);
                var mediaObjects = media.Select(m => new { Path = m.Path }).ToArray();
                return Json(new { success = true, story = story, storyimage = mediaObjects });
            }
            else if(story != null && story.Status == "pending")
            {
                return Json(new { success = false});
            }
            else
            {
                return Json(new { success = "notadded" });
            }
        }

        public IActionResult VolunteeringTimesheet()
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