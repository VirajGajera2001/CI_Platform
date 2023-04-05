using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.Models;
using CI_Platform.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Repository
{
    public class Userprofile : IUserprofile
    {
        private readonly CIdbcontext _objdb;
        public Userprofile(CIdbcontext objdb)
        {
            _objdb = objdb;
        }
        public List<Skill> skills()
        {
            List<Skill> list = _objdb.Skills.ToList();
            return list;
        }
        public User loginuser(int userid)
        {
            var user = _objdb.Users.FirstOrDefault(u => u.UserId == userid);
            return user;
        }
        public void adduser(Userviewmodel userViewModel, int userid)
        {
            User user = _objdb.Users.FirstOrDefault(u=>u.UserId==userid);
            user.FirstName= userViewModel.FirstName==null?user.FirstName:userViewModel.FirstName;
            user.LastName= userViewModel.LastName==null?user.LastName:userViewModel.LastName;
            user.Avatar= userViewModel.Avatar == null ? user.Avatar : userViewModel.Avatar;
            user.EmployeeId= userViewModel.EmployeeId == null ? user.EmployeeId : userViewModel.EmployeeId;
            user.Department= userViewModel.Department == null ? user.Department : userViewModel.Department;
            user.ManagerDetail=userViewModel.ManagerDetail==null?user.ManagerDetail:userViewModel.ManagerDetail;
            user.Title= userViewModel.Title == null ? user.Title : userViewModel.Title;
            user.ProfileText= userViewModel.ProfileText == null ? user.ProfileText : userViewModel.ProfileText;
            user.WhyIVolunteer=userViewModel.WhyIVolunteer == null ? user.WhyIVolunteer : userViewModel.WhyIVolunteer;
            user.CityId= userViewModel.CityId == null ? user.CityId : userViewModel.CityId;
            user.CountryId= userViewModel.CountryId == null ? user.CountryId : userViewModel.CountryId;
            user.Availability= userViewModel.Availability == null ? user.Availability : userViewModel.Availability;
            user.LinkedInUrl= userViewModel.LinkedInUrl == null ? user.LinkedInUrl : userViewModel.LinkedInUrl;
            _objdb.Users.Update(user);
            _objdb.SaveChanges();
        }
        public void saveskill(string[] skill, int userid)
        {
            var userskill=_objdb.UserSkills.Where(us=>us.UserId == userid).ToList();
            if (userskill.Count==0)
            {
                foreach(var item in skill)
                {
                    UserSkill userSkill = new UserSkill();
                    userSkill.UserId= userid;
                    userSkill.SkillId = long.Parse(item);
                    _objdb.UserSkills.Add(userSkill);
                }
                _objdb.SaveChanges();
            }
            else
            {
                foreach(var item in userskill)
                {
                    _objdb.UserSkills.Remove(item);
                    _objdb.SaveChanges();
                }
                foreach (var item in skill)
                {
                    UserSkill userSkill = new UserSkill();
                    userSkill.UserId = userid;
                    userSkill.SkillId = long.Parse(item);
                    _objdb.UserSkills.Add(userSkill);
                }
                _objdb.SaveChanges();
            }
        }
        public List<Skill> oneuserskill(int userid)
        {
            var userskill = _objdb.UserSkills.Where(us => us.UserId == userid).ToList();
            List<Skill> skills=_objdb.Skills.ToList();
            var oneuserskill=(from u in userskill join s in skills on u.SkillId equals s.SkillId select s).ToList();
            return oneuserskill;
        }
        public bool savePassword(string old, string newp, string confp, int userid)
        {
            var user=_objdb.Users.FirstOrDefault(u => u.UserId == userid);
            if(user.Password==old)
            {
                user.Password=newp;
                _objdb.Users.Update(user);
                _objdb.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<Mission> missionstime(int userId)
        {
            var missionappliedbyuser = _objdb.MissionApplications.Where(ma => ma.UserId == userId).ToList();
            List<Mission> missions = _objdb.Missions.Where(m=>m.MissionType=="time").ToList();
            List<Mission> appliedbyuser = (from ma in missionappliedbyuser join ms in missions on ma.MissionId equals ms.MissionId select ms).ToList();
            return appliedbyuser;
        }
        public List<Mission> missionsgoal(int userId)
        {
            var missionappliedbyuser = _objdb.MissionApplications.Where(ma => ma.UserId == userId).ToList();
            List<Mission> missions = _objdb.Missions.Where(m => m.MissionType == "goal").ToList();
            List<Mission> appliedbyuser = (from ma in missionappliedbyuser join ms in missions on ma.MissionId equals ms.MissionId select ms).ToList();
            return appliedbyuser;
        }
        public void timesheet(TimesheetViewModel timesheetViewModel, int userid)
        {
            if (timesheetViewModel.TimesheetId == 0)
            {
                Timesheet timesheet = new Timesheet();
                timesheet.UserId = userid;
                timesheet.MissionId = timesheetViewModel.MissionId;
                timesheet.Action = int.Parse(timesheetViewModel.Action);
                timesheet.DateVolunteered = timesheetViewModel.DateVolunteered;
                timesheet.Notes = timesheetViewModel.Notes;
                _objdb.Timesheets.Add(timesheet);
                _objdb.SaveChanges();
            }
            else
            {
                var find = _objdb.Timesheets.FirstOrDefault(ts => ts.TimesheetId == timesheetViewModel.TimesheetId);
                find.MissionId= timesheetViewModel.MissionId;
                find.Action = int.Parse(timesheetViewModel.Action);
                find.DateVolunteered = timesheetViewModel.DateVolunteered;
                find.Notes = timesheetViewModel.Notes;
                _objdb.Timesheets.Update(find);
                _objdb.SaveChanges();
            }
        }
        public List<Timesheet> timesheetlist(int userId)
        {
            var usersheet=_objdb.Timesheets.Where(ts=>ts.UserId==userId && ts.Action>0).ToList();
            return usersheet;
        }
        public Timesheet findgoalrecord(int timesheetid)
        {
            var find=_objdb.Timesheets.FirstOrDefault(ts=>ts.TimesheetId==timesheetid);
            return find;
        }
        public void deletegoalrecord(int timesheetid)
        {
            var find = _objdb.Timesheets.FirstOrDefault(ts => ts.TimesheetId == timesheetid);
            _objdb.Timesheets.Remove(find);
            _objdb.SaveChanges();
        }
        public void sheetime(TimesheetViewModel timesheetViewModel, int userid)
        {
            if (timesheetViewModel.TimesheetId == 0)
            {
                Timesheet timesheet = new Timesheet();
                timesheet.UserId = userid;
                timesheet.MissionId = timesheetViewModel.MissionId;
                timesheet.Time = timesheetViewModel.Timehour + ":" + timesheetViewModel.Timeminute;
                timesheet.DateVolunteered = timesheetViewModel.DateVolunteered;
                timesheet.Notes = timesheetViewModel.Notes;
                timesheet.Action = 0;
                _objdb.Timesheets.Add(timesheet);
                _objdb.SaveChanges();
            }
            else
            {
                var find = _objdb.Timesheets.FirstOrDefault(ts => ts.TimesheetId == timesheetViewModel.TimesheetId);
                find.MissionId= timesheetViewModel.MissionId;
                find.Action = 0;
                find.DateVolunteered = timesheetViewModel.DateVolunteered;
                find.Notes = timesheetViewModel.Notes;
                find.Time = timesheetViewModel.Timehour + ":" + timesheetViewModel.Timeminute;
                _objdb.Timesheets.Update(find);
                _objdb.SaveChanges();
            }
        }
        public List<Timesheet> timesheetlistTime(int userId)
        {
            var list = _objdb.Timesheets.Where(ts => ts.UserId == userId && ts.Action == 0).ToList();
            return list;
        }
        public Timesheet findtimerecord(int timesheetid)
        {
            var find = _objdb.Timesheets.FirstOrDefault(ts => ts.TimesheetId == timesheetid);
            return find;
        }
    }
}
