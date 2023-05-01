using CI_Platform.Entities.DataModels;
using CI_Platform.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Repository
{
    public class Landing:ILanding
    {
        private readonly CIdbcontext _objdb;
        public Landing(CIdbcontext objdb)
        {
            _objdb = objdb;
        }
        public List<Mission> missions()
        {
            List<Mission> missions = _objdb.Missions.Where(ms=>ms.DeletedAt==null).ToList();
            return missions;
        }
        public List<City> cities()
        {
            List<City> cities = _objdb.Cities.OrderBy(c=>c.Name).ToList();
            return cities;
        }
        public List<Country> countries1()
        {
            List<Country> countries1 = _objdb.Countries.OrderBy(co=>co.Name).ToList();
            return countries1;
        }
        public List<MissionTheme> missionThemes()
        {
            List<MissionTheme> missionThemes = _objdb.MissionThemes.ToList();
            return missionThemes;
        }
        public List<Skill> skill()
        {
            List<Skill> skills=_objdb.Skills.ToList();
            return skills;
        }
        public List<MissionSkill> missionskill()
        {
            List<MissionSkill> missionSkills=_objdb.MissionSkills.ToList();
            return missionSkills;
        }
        public City cityname(City citie)
        {
            var cityname = _objdb.Cities.FirstOrDefault(c => c.CityId == citie.CityId);
            return cityname;
        }
        public Country countries(Country countries)
        {
            var countryname = _objdb.Countries.FirstOrDefault(co => co.CountryId == countries.CountryId);
            return countryname;
        }
        public MissionTheme missionthemes(MissionTheme themes)
        {
            var themename = _objdb.MissionThemes.FirstOrDefault(t => t.MissionThemeId == themes.MissionThemeId);
            return themename;
        }
        public IEnumerable<MissionRating> missionratings(Mission mission)
        {
            var rating1 = _objdb.MissionRatings.Where(rt => rt.MissionId == mission.MissionId);
            return rating1;
        }
        public IEnumerable<FavouriteMission> favouritemissions(Mission mission, int userId)
        {
            var isfav = _objdb.FavouriteMissions.Where(fm => fm.MissionId == mission.MissionId && fm.UserId == userId).ToList();
            return isfav;
        }
        public City cityname(Mission mission)
        {
            var city = _objdb.Cities.FirstOrDefault(c => c.CityId == mission.CityId);
            return city;
        }
        public MissionTheme missionthemes(Mission mission)
        {
            var themes = _objdb.MissionThemes.FirstOrDefault(t => t.MissionThemeId == mission.ThemeId);
            return themes;
        }
        public Country countries(Mission mission)
        {
            var country = _objdb.Countries.FirstOrDefault(co => co.CountryId == mission.CountryId);
            return country;
        }
        public MissionMedium missionmedia(Mission mission)
        {
            var media = _objdb.MissionMedia.FirstOrDefault(mi => mi.MissionId == mission.MissionId);
            return media;
        }
        public GoalMission goalmissions(Mission mission)
        {
            var goalvalue = _objdb.GoalMissions.FirstOrDefault(gm => gm.MissionId == mission.MissionId);
            return goalvalue;
        }
        public List<FavouriteMission> isfav(int userId)
        {
            List<FavouriteMission> isfav = _objdb.FavouriteMissions.Where(fm => fm.UserId == userId).ToList();
            return isfav;
        }
        public MissionApplication applied(int userId,long MissionId)
        {
            var applylist = _objdb.MissionApplications.FirstOrDefault(ma => ma.UserId == userId &&ma.MissionId==MissionId);
            return applylist;
        }
        public List<MissionApplication> misapplied(Mission mission)
        {
            var misapplied = _objdb.MissionApplications.Where(ma => ma.MissionId == mission.MissionId).ToList();
            return misapplied;
        }
        public long goalrecord(long missionId)
        {
            List<Timesheet> timesheets = _objdb.Timesheets.Where(t=>t.MissionId==missionId).ToList();
            if (timesheets.Count>0)
            {
                long j = 0;
                for(int i = 0; i < timesheets.Count; i++)
                {
                    j = (long)(j + timesheets[i].Action);
                }
                return j;
            }
            else
            {
                return 0;
            }
        }
        public Mission finddate(long missionid)
        {
            var mission=_objdb.Missions.FirstOrDefault(ma => ma.MissionId==missionid);
            return mission;
        }
    }
}
