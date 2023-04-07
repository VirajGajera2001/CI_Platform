using CI_Platform.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Interface
{
    public interface ILanding
    {
        public List<Mission> missions();
        public List<City> cities();
        public List<Country> countries1();
        public List<MissionTheme> missionThemes();
        public City cityname(City citie);
        public Country countries(Country countries);
        public MissionTheme missionthemes(MissionTheme themes);
        public IEnumerable<MissionRating> missionratings(Mission mission);
        public IEnumerable<FavouriteMission> favouritemissions(Mission mission, int userId);
        public City cityname(Mission mission);
        public MissionTheme missionthemes(Mission mission);
        public Country countries(Mission mission);
        public MissionMedium missionmedia(Mission mission);
        public GoalMission goalmissions(Mission mission);
        public List<FavouriteMission> isfav(int userId);
        public MissionApplication applied(int userId,long MissionId);
        public List<Skill> skill();
        public List<MissionSkill> missionskill();
        public List<MissionApplication> misapplied(Mission mission);
    }

}