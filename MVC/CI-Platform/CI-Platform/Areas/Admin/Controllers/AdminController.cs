using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.AdminModels;
using CI_Platform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace CI_Platform.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IAdmins _objAdmin;
        public AdminController(ILogger<AdminController> logger, IAdmins objAdmin)
        {
            _logger = logger;
            _objAdmin = objAdmin;
        }
        public IActionResult Users()
        {
            HttpContext.Session.SetInt32("Nav", 1);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            List<User> users = _objAdmin.alluser();
            List<City> cities = _objAdmin.allcity();
            List<Country> countries = _objAdmin.allcountry();
            UserView user = new UserView();
            user.userdata = users;
            ViewBag.City = cities;
            ViewBag.Country = countries;
            return View(user);
        }
        [HttpPost]
        public IActionResult Users(UserView userView)
        {
            _objAdmin.saveuser(userView);
            return RedirectToAction("Users", "Admin");
        }
        public IActionResult CMS()
        {
            HttpContext.Session.SetInt32("Nav", 2);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            List<CmsPage> cmsPages = _objAdmin.cmsrecordall();
            CMSView cMSView = new CMSView();
            cMSView.CmsPages = cmsPages;
            return View(cMSView);
        }
        [HttpPost]
        public IActionResult CMS(CMSView cmsView)
        {
            _objAdmin.cmsadd(cmsView);
            return RedirectToAction("CMS", "Admin");
        }
        public IActionResult Mission()
        {
            HttpContext.Session.SetInt32("Nav", 3);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            MissionView missionView= new MissionView();
            List<Mission> missions = _objAdmin.allmission();
            List<MissionTheme> missionThemes = _objAdmin.alltheme();
            List<Country> countries=_objAdmin.allcountry();
            List<City> citys = _objAdmin.allcity();
            List<Skill> skills = _objAdmin.skilllist();
            missionView.missions = missions;
            missionView.missionThemes = missionThemes;
            missionView.countries = countries;
            missionView.citys = citys;
            missionView.skills= skills;
            return View(missionView);
        }
        [HttpPost]
        public IActionResult Mission(MissionView missionView, string[] selectedValues, string[] dataUrls)
        {
            return RedirectToAction("Mission","Admin");
        }
        public IActionResult Theme()
        {
            HttpContext.Session.SetInt32("Nav", 4);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            List<MissionTheme> missionThemes = _objAdmin.alltheme();
            ThemeView themeView = new ThemeView();
            themeView.missionThemes = missionThemes;
            return View(themeView);
        }
        [HttpPost]
        public IActionResult Theme(ThemeView themeView)
        {
            _objAdmin.addtheme(themeView);
            return RedirectToAction("Theme", "Admin");
        }
        public IActionResult Skill()
        {
            HttpContext.Session.SetInt32("Nav", 5);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            List<Skill> skills = _objAdmin.skilllist();
            SkillView skillView = new SkillView();
            skillView.skills = skills;
            return View(skillView);
        }
        [HttpPost]
        public IActionResult Skill(SkillView skillView)
        {
            _objAdmin.skillsave(skillView);
            return RedirectToAction("Skill", "Admin");
        }
        public IActionResult Application()
        {
            HttpContext.Session.SetInt32("Nav", 6);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            List<MissionApplicationView> missionApplicationView = new List<MissionApplicationView>();
            List<MissionApplication> missionApplications = _objAdmin.allmissionapp();
            List<Mission> missions = _objAdmin.allmission();
            List<User> users = _objAdmin.alluser();
            var missionapp = (from ma in missionApplications join ms in missions on ma.MissionId equals ms.MissionId join u in users on ma.UserId equals u.UserId select new { Title = ms.Title, MissionId = ma.MissionId, UserId = ma.UserId, FirstName = u.FirstName, LastName = u.LastName, AppliedAt = ma.AppliedAt, MissionApplicationId=ma.MissionApplicationId }).ToList();
            foreach (var item in missionapp)
            {
                MissionApplicationView missionApplicationView1 = new MissionApplicationView();
                missionApplicationView1.MissionApplicationId = item.MissionApplicationId;
                missionApplicationView1.Title = item.Title;
                missionApplicationView1.MissionId = item.MissionId;
                missionApplicationView1.UserId = item.UserId;
                missionApplicationView1.FirstName = item.FirstName;
                missionApplicationView1.LastName = item.LastName;
                missionApplicationView1.AppliedAt = item.AppliedAt;
                missionApplicationView.Add(missionApplicationView1);
            }
            return View(missionApplicationView);
        }
        public IActionResult Story()
        {
            HttpContext.Session.SetInt32("Nav", 7);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            List<StoryView> storyViews = new List<StoryView>();
            List<Mission> missions = _objAdmin.allmission();
            List<User> users = _objAdmin.alluser();
            List<Story> stories = _objAdmin.allstory();
            var storyrecord = (from st in stories join u in users on st.UserId equals u.UserId join ms in missions on st.MissionId equals ms.MissionId select new { StoryTitle = st.Title, FirstName = u.FirstName, LastName = u.LastName, MissionTitle = ms.Title, MissionId = st.MissionId, UserId = st.UserId, StoryId = st.StoryId }).ToList();
            foreach (var item in storyrecord)
            {
                StoryView storyView = new StoryView();
                storyView.MissionTitle = item.MissionTitle;
                storyView.StoryTitle = item.StoryTitle;
                storyView.StoryId = item.StoryId;
                storyView.UserId = item.UserId;
                storyView.FirstName = item.FirstName;
                storyView.LastName = item.LastName;
                storyViews.Add(storyView);
            }
            return View(storyViews);
        }
        public IActionResult Banner()
        {
            HttpContext.Session.SetInt32("Nav", 8);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            List<Banner> bannerViews2 = _objAdmin.bannerlist();
            BannerView bannerView = new BannerView();
            bannerView.banners=bannerViews2;
            return View(bannerView);
        }
        [HttpPost]
        public IActionResult Banner(BannerView bannerView)
        {
            _objAdmin.bannersave(bannerView);
            return RedirectToAction("Banner", "Admin");
        }
        [HttpPost]
        public IActionResult UserEdit(long userId)
        {
            var user = _objAdmin.edituser(userId);
            return Json(user);
        }
        [HttpPost]
        public IActionResult UserDelete(long userId)
        {
            _objAdmin.deleteuser(userId);
            return Json(null);
        }
        [HttpPost]
        public IActionResult CmsEdit(long cmsId)
        {
            var cms = _objAdmin.findcms(cmsId);
            return Json(cms);
        }
        [HttpPost]
        public IActionResult CmsDelete(long cmsId)
        {
            _objAdmin.deletecms(cmsId);
            return Json(null);
        }
        [HttpPost]
        public IActionResult ThemeEdit(long themeId)
        {
            var theme = _objAdmin.themeedit(themeId);
            return Json(theme);
        }
        [HttpPost]
        public IActionResult ThemeDelete(long themeId)
        {
            _objAdmin.deletetheme(themeId);
            return Json(null);
        }
        [HttpPost]
        public IActionResult SkillEdit(long skillId)
        {
            var skill = _objAdmin.skilledit(skillId);
            return Json(skill);
        }
        [HttpPost]
        public IActionResult SkillDelete(long skillId)
        {
            _objAdmin.deleteskill(skillId);
            return Json(null);
        }
        [HttpPost]
        public IActionResult ApproveStory(long storyId)
        {
            _objAdmin.approve(storyId);
            return Json(null);
        }
        [HttpPost]
        public IActionResult RejectStory(long storyId)
        {
            _objAdmin.reject(storyId);
            return Json(null);
        }
        [HttpPost]
        public IActionResult ApproveMission(long applicationId)
        {
            _objAdmin.approvemis(applicationId);
            return Json(null);
        }
        [HttpPost]
        public IActionResult DeclineMission(long applicationId)
        {
            _objAdmin.declinemis(applicationId);
            return Json(null);
        }
        [HttpPost]
        public IActionResult BannerEdit(long bannerId)
        {
            var banner = _objAdmin.banner(bannerId);
            return Json(banner);
        }
        [HttpPost]
        public IActionResult BannerDelete(long bannerId)
        {
            _objAdmin.deletebanner(bannerId);
            return Json(null);
        }
    }
}
