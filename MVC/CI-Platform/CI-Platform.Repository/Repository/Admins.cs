using CI_Platform.Entities.AdminModels;
using CI_Platform.Entities.DataModels;
using CI_Platform.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Repository
{
    public class Admins:IAdmins
    {
        private readonly CIdbcontext _objdb;
        public Admins(CIdbcontext objdb)
        {
            _objdb = objdb;
        }
        public List<User> alluser()
        {
            List<User> users = _objdb.Users.Where(u=>u.DeletedAt==null).ToList();
            return users;
        }
        public List<CmsPage> cmsrecordall()
        {
            List<CmsPage> cmsPages= _objdb.CmsPages.ToList();
            return cmsPages;
        }
        public List<Mission> allmission()
        {
            List<Mission> missions= _objdb.Missions.Where(ms=>ms.DeletedAt==null).ToList();
            return missions;
        }
        public List<MissionApplication> allmissionapp()
        {
            List<MissionApplication> missionApplications= _objdb.MissionApplications.Where(ma=>ma.ApprovalStatus == "PENDING").ToList();
            return missionApplications;
        }
        public List<Story> allstory()
        {
            List<Story> stories=_objdb.Stories.Where(st=>st.Status=="pending"&&st.DeletedAt==null).ToList();
            return stories;
        }
        public User edituser(long userId)
        {
            var user= _objdb.Users.FirstOrDefault(u=> u.UserId == userId);
            return user;
        }
        public List<City> allcity()
        {
            List<City> cities = _objdb.Cities.ToList();
            return cities;
        }
        public List<Country> allcountry()
        {
            List<Country> countries=_objdb.Countries.ToList();
            return countries;
        }
        public void saveuser(UsersView userView)
        {
            var user=_objdb.Users.FirstOrDefault(u=>u.UserId == userView.UserId);
            if (user != null)
            {
                user.FirstName= userView.FirstName;
                user.LastName= userView.LastName;
                user.Email= userView.Email;
                user.Password= userView.Password;
                user.EmployeeId= userView.EmployeeId;
                user.Department= userView.Department;
                user.CityId= userView.CityId;
                user.CountryId= userView.CountryId;
                user.ProfileText= userView.ProfileText;
                if (userView.Status == "Active")
                {
                    user.Status = true;
                }
                else
                {
                    user.Status= false;
                }
                user.Avatar= userView.Avatar;
                _objdb.Users.Update(user);
                _objdb.SaveChanges();
            }
            else
            {
                User user1 = new User();
                user1.FirstName = userView.FirstName;
                user1.LastName = userView.LastName;
                user1.Email = userView.Email;
                user1.Password = userView.Password;
                user1.EmployeeId = userView.EmployeeId;
                user1.Department = userView.Department;
                user1.CityId = userView.CityId;
                user1.CountryId = userView.CountryId;
                user1.ProfileText = userView.ProfileText;
                if (userView.Status == "Active")
                {
                    user1.Status = true;
                }
                else
                {
                    user1.Status = false;
                }
                user1.Avatar = userView.Avatar;
                _objdb.Users.Add(user1);
                _objdb.SaveChanges();
            }
        }
        public void deleteuser(long userId)
        {
            var user = _objdb.Users.FirstOrDefault(u=>u.UserId == userId);
            if (user != null)
            {
                user.Status = false;
                user.DeletedAt = DateTime.Now;
                _objdb.Users.Update(user);
                _objdb.SaveChanges();
            }
        }
        public CmsPage findcms(long cmsId)
        {
            var cms=_objdb.CmsPages.FirstOrDefault(cm=>cm.CmsPageId== cmsId);
            return cms;
        }
        public void cmsadd(CI_Platform.Entities.AdminModels.CMSView cmsView)
        {
            var cms = _objdb.CmsPages.FirstOrDefault(cm => cm.CmsPageId == cmsView.CmsPageId);
            if (cms != null)
            {
                cms.Title=cmsView.Title;
                cms.Description=cmsView.Description;
                cms.Slug=cmsView.Slug;
                if (cmsView.Status == "Active")
                {
                    cms.Status = true;
                }
                else
                {
                    cms.Status = false;
                }
                _objdb.CmsPages.Update(cms);
                _objdb.SaveChanges();
            }
            else
            {
                CmsPage cmsPage = new CmsPage();
                cmsPage.Title = cmsView.Title;
                cmsPage.Description = cmsView.Description;
                cmsPage.Slug = cmsView.Slug;
                if (cmsView.Status == "Active")
                {
                    cmsPage.Status = true;
                }
                else
                {
                    cmsPage.Status = false;
                }
                _objdb.CmsPages.Update(cmsPage);
                _objdb.SaveChanges();
            }
        }
        public void deletecms(long cmsId)
        {
            var cms= _objdb.CmsPages.FirstOrDefault(cm=>cm.CmsPageId==cmsId);
            _objdb.CmsPages.Remove(cms);
            _objdb.SaveChanges();
        }
        public List<MissionTheme> alltheme()
        {
            List<MissionTheme> list = _objdb.MissionThemes.Where(mt=>mt.DeletedAt==null).ToList();
            return list;
        }
        public MissionTheme themeedit(long themeId)
        {
            var theme=_objdb.MissionThemes.FirstOrDefault(mt=>mt.MissionThemeId==themeId);
            return theme;
        }
        public void addtheme(ThemeView themeView)
        {
            var theme = _objdb.MissionThemes.FirstOrDefault(mt => mt.MissionThemeId == themeView.MissionThemeId);
            if (theme != null)
            {
                theme.Title = themeView.Title;
                if (themeView.Status == "Active")
                {
                    theme.Status = 1;
                }
                else
                {
                    theme.Status = 0;
                }
                _objdb.MissionThemes.Update(theme);
                _objdb.SaveChanges();
            }
            else
            {
                MissionTheme missionTheme= new MissionTheme();
                missionTheme.Title = themeView.Title;
                if (themeView.Status == "Active")
                {
                    missionTheme.Status = 1;
                }
                else
                {
                    missionTheme.Status = 0;
                }
                _objdb.MissionThemes.Add(missionTheme);
                _objdb.SaveChanges();
            }
        }
        public void deletetheme(long themeId)
        {
            List<Mission> missions = _objdb.Missions.Where(ms => ms.ThemeId == themeId).ToList();
            if(missions.Count > 0)
            {
                var find = _objdb.MissionThemes.FirstOrDefault(mt => mt.MissionThemeId == themeId);
                find.DeletedAt = DateTime.Now;
                _objdb.MissionThemes.Update(find);
                _objdb.SaveChanges();
            }
            else
            {
                var find = _objdb.MissionThemes.FirstOrDefault(mt => mt.MissionThemeId == themeId);
                _objdb.MissionThemes.Remove(find);
                _objdb.SaveChanges();
            }
        }
        public List<Skill> skilllist()
        {
            List<Skill> list = _objdb.Skills.Where(sk => sk.DeletedAt == null).ToList();
            return list;
        }
        public Skill skilledit(long skillId)
        {
            var skill = _objdb.Skills.FirstOrDefault(sk => sk.SkillId == skillId);
            return skill;
        }
        public void deleteskill(long skillId)
        {
            List<UserSkill> userSkills = _objdb.UserSkills.Where(us => us.SkillId == skillId).ToList();
            if (userSkills.Count > 0)
            {
                var skillmatch = _objdb.Skills.FirstOrDefault(sk => sk.SkillId == skillId);
                skillmatch.DeletedAt= DateTime.Now;
                _objdb.Skills.Update(skillmatch);
                _objdb.SaveChanges();
            }
            else
            {
                var skill = _objdb.Skills.FirstOrDefault(sk => sk.SkillId == skillId);
                skill.DeletedAt = DateTime.Now;
                _objdb.Skills.Update(skill);
                _objdb.SaveChanges();
            }
        }
        public void skillsave(CI_Platform.Entities.AdminModels.SkillView skillView)
        {
            var skill = _objdb.Skills.FirstOrDefault(sk => sk.SkillId == skillView.SkillId);
            if (skill != null)
            {
                skill.SkillName = skillView.SkillName;
                skill.UpdatedAt=DateTime.Now;
                if (skillView.Status == "Active")
                {
                    skill.Status = 1;
                }
                else
                {
                    skill.Status = 0;
                }
                _objdb.Skills.Update(skill);
                _objdb.SaveChanges();
            }
            else
            {
                Skill skill1=new Skill();
                skill1.SkillName = skillView.SkillName;
                if (skillView.Status == "Active")
                {
                    skill1.Status = 1;
                }
                else
                {
                    skill1.Status = 0;
                }
                _objdb.Skills.Add(skill1);
                _objdb.SaveChanges();
            }
        }
        public void approve(long storyId)
        {
            var story = _objdb.Stories.FirstOrDefault(st => st.StoryId == storyId);
            if (story != null)
            {
                story.Status = "PUBLISHED";
                story.UpdatedAt = DateTime.Now;
                _objdb.Stories.Update(story);
                _objdb.SaveChanges();
            }
        }
        public void reject(long storyId)
        {
            var story = _objdb.Stories.FirstOrDefault(st => st.StoryId == storyId);
            if (story != null)
            {
                story.Status = "DECLINED";
                story.UpdatedAt = DateTime.Now;
                _objdb.Stories.Update(story);
                _objdb.SaveChanges();
            }
        }
        public void approvemis(long missionappId)
        {
            var mis=_objdb.MissionApplications.FirstOrDefault(ma=>ma.MissionApplicationId==missionappId);
            if (mis != null)
            {
                mis.ApprovalStatus = "APPROVAL";
                mis.UpdatedAt = DateTime.Now;
            }
            _objdb.MissionApplications.Update(mis);
            _objdb.SaveChanges();
        }
        public void declinemis(long missionappId)
        {
            var mis = _objdb.MissionApplications.FirstOrDefault(ma => ma.MissionApplicationId == missionappId);
            if (mis != null)
            {
                mis.ApprovalStatus = "DECLINE";
                mis.UpdatedAt = DateTime.Now;
            }
            _objdb.MissionApplications.Update(mis);
            _objdb.SaveChanges();
        }
        public List<Banner> bannerlist()
        {
            var list = _objdb.Banners.Where(b=>b.DeletedAt==null).ToList();
            return list;
        }
        public Banner banner(long bannerId)
        {
            var banner=_objdb.Banners.FirstOrDefault(b=>b.BannerId==bannerId);
            return banner;
        }
        public void bannersave(CI_Platform.Entities.AdminModels.BannerView bannerView)
        {
            var banner = _objdb.Banners.FirstOrDefault(b => b.BannerId == bannerView.BannerId);
            if(banner != null)
            {
                banner.Text=bannerView.Text;
                banner.SortOrder=bannerView.SortOrder;
                banner.Image=bannerView.Image;
                _objdb.Banners.Update(banner);
            }
            else
            {
                Banner banner1=new Banner();
                banner1.Text=bannerView.Text;
                banner1.SortOrder=bannerView.SortOrder;
                banner1.Image=bannerView.Image;
                _objdb.Banners.Add(banner1);
            }
            _objdb.SaveChanges();
        }
        public void deletebanner(long bannerId)
        {
            var banner=_objdb.Banners.FirstOrDefault(b=>b.BannerId==bannerId);
            if(banner != null )
            {
                banner.DeletedAt = DateTime.Now;
                _objdb.Banners.Update(banner);
                _objdb.SaveChanges();
            }
        }
        public void savemission(MissionView missionView, string[] selectedValues, string[] dataUrls, string[] docFiles, string[] docName, string videoUrls)
        {
            var findmission = _objdb.Missions.FirstOrDefault(m => m.MissionId == missionView.MissionId);
            if (findmission != null )
            {
                findmission.Title = missionView.Title;
                findmission.ThemeId = missionView.ThemeId;
                findmission.CityId = missionView.CityId;
                findmission.CountryId = missionView.CountryId;
                findmission.Description = missionView.Description;
                findmission.ShortDescription = missionView.ShortDescription;
                findmission.StartDate = missionView.StartDate;
                findmission.EndDate = missionView.EndDate;
                findmission.MissionType = missionView.MissionType;
                findmission.OrganizationName = missionView.OrganizationName;
                findmission.OrganizationDetalis = missionView.OrganizationDetalis;
                findmission.Availability = missionView.Availability;
                findmission.SeatsAvailable = missionView.SeatsAvailable;
                findmission.Deadline = missionView.Deadline;
                findmission.UpdatedAt = DateTime.Now;
                findmission.Status = true;
                _objdb.Missions.Update(findmission);
                List<MissionSkill> missionSkills = _objdb.MissionSkills.Where(ms => ms.MissionId == findmission.MissionId).ToList();
                foreach(var item in missionSkills)
                {
                    _objdb.MissionSkills.Remove(item);
                }
                foreach (var item in selectedValues)
                {
                    MissionSkill missionSkill = new MissionSkill();
                    missionSkill.MissionId = findmission.MissionId;
                    missionSkill.SkillId = long.Parse(item);
                    _objdb.Add(missionSkill);
                }
                List<MissionMedium> missionMedia = _objdb.MissionMedia.Where(mm => mm.MissionId == findmission.MissionId).ToList();
                foreach(var item in missionMedia)
                {
                    _objdb.MissionMedia.Remove(item);
                }
                foreach (var item in dataUrls)
                {
                    MissionMedium missionMedium = new MissionMedium();
                    missionMedium.MissionId = findmission.MissionId;
                    missionMedium.MediaPath = item;
                    missionMedium.MediaName = "logo";
                    missionMedium.MediaType = "imag";
                    _objdb.Add(missionMedium);
                }
                MissionMedium missionMedium1 = new MissionMedium();
                missionMedium1.MissionId = findmission.MissionId;
                missionMedium1.MediaPath = videoUrls;
                missionMedium1.MediaName = "logo";
                missionMedium1.MediaType = "video";
                _objdb.Add(missionMedium1);
                List<MissionDocument> missionDocuments = _objdb.MissionDocuments.Where(md => md.MissionId == findmission.MissionId).ToList();
                foreach(var item in missionDocuments)
                {
                    _objdb.MissionDocuments.Remove(item);
                }
                for (int i = 0; i < docFiles.Length; i++)
                {
                    var file = docFiles[i];
                    var name = docName[i];
                    MissionDocument missionDocument = new MissionDocument();
                    missionDocument.MissionId = findmission.MissionId;
                    missionDocument.DocumentName = name;
                    missionDocument.DocumentPath = file;
                    missionDocument.DocumentType = "document";
                    _objdb.MissionDocuments.Add(missionDocument);
                }
                _objdb.SaveChanges();
            }
            else
            {
                Mission mission = new Mission();
                mission.Title = missionView.Title;
                mission.ThemeId = missionView.ThemeId;
                mission.CityId = missionView.CityId;
                mission.CountryId = missionView.CountryId;
                mission.Description = missionView.Description;
                mission.ShortDescription = missionView.ShortDescription;
                mission.StartDate = missionView.StartDate;
                mission.EndDate = missionView.EndDate;
                mission.MissionType= missionView.MissionType;
                mission.OrganizationName = missionView.OrganizationName;
                mission.OrganizationDetalis= missionView.OrganizationDetalis;
                mission.Availability= missionView.Availability;
                mission.SeatsAvailable= missionView.SeatsAvailable;
                mission.Deadline= missionView.Deadline;
                mission.Status = true;
                _objdb.Missions.Add(mission);
                _objdb.SaveChanges();
                foreach(var item in selectedValues)
                {
                    MissionSkill missionSkill = new MissionSkill();
                    missionSkill.MissionId = mission.MissionId;
                    missionSkill.SkillId = long.Parse(item);
                    _objdb.Add(missionSkill);
                }
                foreach(var item in dataUrls)
                {
                    MissionMedium missionMedium= new MissionMedium();
                    missionMedium.MissionId = mission.MissionId;
                    missionMedium.MediaPath = item;
                    missionMedium.MediaName="logo";
                    missionMedium.MediaType = "imag";
                    _objdb.Add(missionMedium);
                }
                MissionMedium missionMedium1= new MissionMedium();
                missionMedium1.MissionId = mission.MissionId;
                missionMedium1.MediaPath = videoUrls;
                missionMedium1.MediaName = "logo";
                missionMedium1.MediaType = "video";
                _objdb.Add(missionMedium1);
                for(int i = 0; i < docFiles.Length; i++)
                {
                    var file = docFiles[i];
                    var name = docName[i];
                    MissionDocument missionDocument = new MissionDocument();
                    missionDocument.MissionId = mission.MissionId;
                    missionDocument.DocumentName = name;
                    missionDocument.DocumentPath = file;
                    missionDocument.DocumentType = "document";
                    _objdb.MissionDocuments.Add(missionDocument);
                }
                _objdb.SaveChanges();
            }

        }
        public MissionView findmission(long missionId)
        {
            MissionView missionView = new MissionView();
            var mission=_objdb.Missions.FirstOrDefault(m=>m.MissionId== missionId);
            if (mission != null)
            {
                missionView.MissionId = missionId;
                missionView.CityId = mission.CityId;
                missionView.CountryId = mission.CountryId;
                missionView.ThemeId = mission.ThemeId;
                missionView.Title = mission.Title;
                missionView.Description = mission.Description;
                missionView.ShortDescription = mission.ShortDescription;
                missionView.StartDate = mission.StartDate;
                missionView.EndDate = mission.EndDate;
                missionView.Deadline = mission.Deadline;
                missionView.SeatsAvailable = mission.SeatsAvailable;
                missionView.MissionType = mission.MissionType;
                missionView.OrganizationName = mission.OrganizationName;
                missionView.OrganizationDetalis= mission.OrganizationDetalis;
                missionView.Availability = mission.Availability;
                List<MissionSkill> missionSkills = _objdb.MissionSkills.Where(ms => ms.MissionId == missionId && ms.DeletedAt==null).ToList();
                missionView.missionSkills= missionSkills;
                List<MissionMedium> missionMedia = _objdb.MissionMedia.Where(mm => mm.MissionId == missionId && mm.DeletedAt==null).ToList();
                missionView.missionMedia= missionMedia;
                List<MissionDocument> missionDocuments = _objdb.MissionDocuments.Where(md => md.MissionId == missionId && md.DeletedAt == null).ToList();
                missionView.missionDocuments= missionDocuments;
            }
            return missionView;
        }
        public void deletemission(long missionId)
        {
            var findmission = _objdb.Missions.FirstOrDefault(m => m.MissionId == missionId);
            if (findmission != null)
            {
                findmission.DeletedAt= DateTime.Now;
                findmission.Status = false;
                _objdb.Missions.Update(findmission);
                List<MissionSkill> missionSkills = _objdb.MissionSkills.Where(ms => ms.MissionId == missionId).ToList();
                foreach(var item in missionSkills)
                {
                    item.DeletedAt=DateTime.Now;
                    _objdb.MissionSkills.Update(item);
                }
                List<MissionMedium> missionMedia = _objdb.MissionMedia.Where(mm => mm.MissionId == missionId).ToList();
                foreach(var item in missionMedia)
                {
                    item.DeletedAt= DateTime.Now;
                    _objdb.MissionMedia.Update(item);
                }
                List<MissionDocument> missionDocuments = _objdb.MissionDocuments.Where(md => md.MissionId == missionId).ToList();
                foreach(var item in missionDocuments)
                {
                    item.DeletedAt= DateTime.Now;
                    _objdb.MissionDocuments.Update(item);
                }
                _objdb.SaveChanges();
            }
        }
        public void deletestory(long storyid)
        {
            var story=_objdb.Stories.FirstOrDefault(s=>s.StoryId== storyid);
            if (story != null)
            {
                story.DeletedAt=DateTime.Now;
                _objdb.Stories.Update(story);
                _objdb.SaveChanges();
            }
        }
    }
}
