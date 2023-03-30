using CI_Platform.Entities.DataModels;
using CI_Platform.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Repository
{
    public class Volunteer : IVolunteer
    {
        private readonly CIdbcontext _objdb;
        public Volunteer(CIdbcontext objdb)
        {
            _objdb = objdb;
        }
        public Mission missions(int MissionId)
        {
            var missions = _objdb.Missions.FirstOrDefault(m => m.MissionId == MissionId);
            return missions;
        }
        public City cities(Mission missions)
        {
            if (missions == null)
            {
                // Handle the case where missions is null
                return null;
            }

            var city = _objdb.Cities.FirstOrDefault(c => c.CityId == missions.CityId);
            return city;
        }

        public MissionTheme missiontheme(Mission missions)
        {
            if (missions == null)
            {
                // Handle the case where missions is null
                return null;
            }
            var theme = _objdb.MissionThemes.FirstOrDefault(mt => mt.MissionThemeId == missions.ThemeId);
            return theme;
        }
        public IEnumerable<Mission> missions(int MissionId, Mission missions)
        {
            var relatedmission = _objdb.Missions.Where(t => t.MissionId != MissionId && t.ThemeId == missions.ThemeId);
            return relatedmission;
        }
        public List<User> users()
        {
            List<User> users = _objdb.Users.ToList();
            return users;
        }
        public List<MissionApplication> missionapp()
        {
            List<MissionApplication> missionapplications = _objdb.MissionApplications.ToList();
            return missionapplications;
        }
        public IEnumerable<MissionRating> missionratings(Mission missions)
        {
            if (missions == null)
            {
                // Handle the case where missions is null
                return null;
            }
            var rating1 = _objdb.MissionRatings.Where(rt => rt.MissionId == missions.MissionId);
            return rating1;
        }
        public GoalMission goalmissions(Mission missions)
        {
            if (missions == null)
            {
                // Handle the case where missions is null
                return null;
            }
            var goalvalue = _objdb.GoalMissions.FirstOrDefault(gm => gm.MissionId == missions.MissionId);
            return goalvalue;
        }
        public List<Mission> missions()
        {
            List<Mission> missionlist = _objdb.Missions.ToList();
            return missionlist;
        }
        public List<Comment> comments()
        {
            List<Comment> comments = _objdb.Comments.ToList();
            return comments;
        }
        public IEnumerable<FavouriteMission> favmissions(Mission missions, long? UserId)
        {
            var favo = _objdb.FavouriteMissions.Where(e => e.MissionId == missions.MissionId && e.UserId == UserId);
            return favo;
        }
        public MissionRating missionrating(Mission missions, long? UserId)
        {
            var prewrating = _objdb.MissionRatings.FirstOrDefault(r => r.MissionId == missions.MissionId && r.UserId == UserId);
            return prewrating;
        }
        public MissionRating ms(long Id, long missionId, string rating)
        {
            MissionRating ratingExists = _objdb.MissionRatings.FirstOrDefault(fm => fm.UserId == Id && fm.MissionId == missionId);
            if (ratingExists != null)
            {
                ratingExists.Rating = rating;
                _objdb.Update(ratingExists);
                _objdb.SaveChanges();
                return ratingExists;
            }
            else
            {
                var ratingele = new MissionRating();
                ratingele.Rating = rating;
                ratingele.UserId = Id;
                ratingele.MissionId = missionId;
                _objdb.Add(ratingele);
                _objdb.SaveChanges();
                return ratingele;
            }
        }
        public MissionRating missionrating(Mission missions, int userId)
        {
            var prewrating = _objdb.MissionRatings.FirstOrDefault(r => r.MissionId == missions.MissionId && r.UserId == userId);
            return prewrating;
        }
        public FavouriteMission favms(long missionId, long Id)
        {
            FavouriteMission favexists = _objdb.FavouriteMissions.FirstOrDefault(fm => fm.UserId == Id && fm.MissionId == missionId);
            if (favexists != null)
            {
                favexists.MissionId = missionId;
                favexists.UserId = Id;
                _objdb.Remove(favexists);
                _objdb.SaveChanges();
                return favexists;
            }
            else
            {
                var favele = new FavouriteMission();
                favele.MissionId = missionId;
                favele.UserId = Id;
                _objdb.Add(favele);
                _objdb.SaveChanges();
                return favele;
            }
        }
        public IEnumerable<FavouriteMission> favmission(Mission missions, int userId)
        {
            var favo = _objdb.FavouriteMissions.Where(e => e.MissionId == missions.MissionId && e.UserId == userId);
            return favo;
        }
        public Comment comments(long missionId, long userId, string commentText)
        {
            Comment comment = _objdb.Comments.FirstOrDefault(cm => cm.MissionId == missionId && cm.UserId == userId);
            if (comment != null)
            {
                var addcomm = new Comment();
                addcomm.MissionId = missionId;
                addcomm.UserId = userId;
                addcomm.CommentText = commentText;
                addcomm.CreatedAt = DateTime.Now;
                _objdb.Add(addcomm);
                _objdb.SaveChangesAsync();
                return comment;
            }
            else
            {
                var addcomm = new Comment();
                addcomm.MissionId = missionId;
                addcomm.UserId = userId;
                addcomm.CommentText = commentText;
                addcomm.CreatedAt = DateTime.Now;
                _objdb.Add(addcomm);
                _objdb.SaveChangesAsync();
                return addcomm;
            }
        }
        public MissionApplication applied(int MissionId, long? UserId)
        {
            var isapplied = _objdb.MissionApplications.FirstOrDefault(ma => ma.MissionId == MissionId && ma.UserId == UserId);
            return isapplied;
        }
        public MissionApplication appliedmis(int MissionId, int UserId)
        {
            var isapplied = _objdb.MissionApplications.FirstOrDefault(ma => ma.MissionId == MissionId && ma.UserId == UserId);
            return isapplied;
        }
        public bool applyMission(int MissionId, int UserId)
        {
            MissionApplication ms=new MissionApplication();
            ms.MissionId = MissionId;
            ms.UserId = UserId;
            ms.AppliedAt=DateTime.Now;
            _objdb.Add(ms);
            _objdb.SaveChanges();
            return true;
        }
        public IEnumerable<MissionDocument> missiondocs(int MissionId)
        {
            var alldocs = _objdb.MissionDocuments.Where(md => md.MissionId == MissionId);
            return alldocs;
        }
        public IEnumerable<FavouriteMission> favouritemissions(Mission mission, long? userId)
        {
            var isfav = _objdb.FavouriteMissions.Where(fm => fm.MissionId == mission.MissionId && fm.UserId == userId).ToList();
            return isfav;
        }
        public MissionApplication applied(long? userId, long MissionId)
        {
            var applylist = _objdb.MissionApplications.FirstOrDefault(ma => ma.UserId == userId && ma.MissionId == MissionId);
            return applylist;
        }
    }
}
