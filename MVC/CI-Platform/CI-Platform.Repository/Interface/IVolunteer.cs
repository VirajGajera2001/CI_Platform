using CI_Platform.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Interface
{
    public interface IVolunteer
    {
        public Mission missions(int MissionId);
        public City cities(Mission missions);
        public MissionTheme missiontheme(Mission missions);
        public IEnumerable<Mission> missions(int MissionId, Mission missions);
        public List<User> users();
        public List<MissionApplication> missionapp();
        public IEnumerable<MissionRating> missionratings(Mission missions);
        public GoalMission goalmissions(Mission missions);
        public List<Mission> missions();
        public List<Comment> comments();
        public IEnumerable<FavouriteMission> favmissions(Mission missions, long? UserId);
        public MissionRating missionrating(Mission missions,long? UserId);
        public MissionRating ms(long Id, long missionId,string rating);
        public MissionRating missionrating(Mission missions, int userId);
        public FavouriteMission favms(long missionId,long Id);
        public IEnumerable<FavouriteMission> favmission(Mission missions, int userId);
        public Comment comments(long missionId, long userId, string commentText);
        public MissionApplication applied(int MissionId, long? UserId);
        public MissionApplication appliedmis(int MissionId, int UserId);
        public bool applyMission(int MissionId, int UserId);
        public IEnumerable<MissionDocument> missiondocs(int MissionId);
    }
}
