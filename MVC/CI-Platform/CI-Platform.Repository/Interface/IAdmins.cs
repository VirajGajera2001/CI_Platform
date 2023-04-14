using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.Models;
using CI_Platform.Entities.AdminModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Interface
{
    public interface IAdmins
    {
        public List<User> alluser();
        public List<CmsPage> cmsrecordall();
        public List<Mission> allmission();
        public List<MissionApplication> allmissionapp();
        public List<Story> allstory();
        public User edituser(long userId);
        public List<City> allcity();
        public List<Country> allcountry();
        public void saveuser(Entities.AdminModels.UserView userView);
        public void deleteuser(long userId);
        public CmsPage findcms(long cmsId);
        public void cmsadd(CI_Platform.Entities.AdminModels.CMSView cmsView);
        public void deletecms(long cmsId);
        public List<MissionTheme> alltheme();
        public MissionTheme themeedit(long themeId);
        public void addtheme(ThemeView themeView);
        public void deletetheme(long themeId);
        public List<Skill> skilllist();
        public Skill skilledit(long skillId);
        public void deleteskill(long skillId);
        public void skillsave(CI_Platform.Entities.AdminModels.SkillView skillView);
        public void approve(long storyId);
        public void reject(long storyId);
        public void approvemis(long missionappId);
        public void declinemis(long missionappId);
    }
}
