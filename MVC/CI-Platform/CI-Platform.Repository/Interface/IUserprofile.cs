using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Interface
{
    public interface IUserprofile
    {
        public List<Skill> skills();
        public User loginuser(int userid);
        public void adduser(UserView userViewModel,int userid);
        public void saveskill(string[] skill,int userid);
        public List<Skill> oneuserskill(int userid);
        public bool savePassword(string old, string newp, string confp, int userid);
        public List<Mission> missionstime(int userId);
        public List<Mission> missionsgoal(int userId);
        public void timesheet(TimesheetViewModel timesheetViewModel, int userid);
        public List<Timesheet> timesheetlist(int userId);
        public Timesheet findgoalrecord(int timesheetid);
        public void deletegoalrecord(int timesheetid);
        public void sheetime(TimesheetViewModel timesheetViewModel, int userid);
        public List<Timesheet> timesheetlistTime(int userId);
        public Timesheet findtimerecord(int timesheetid);
        public void adduser(Entities.AdminModels.UsersView userViewModel, int v);
        public List<City> cities(long? countryId);
    }
}
