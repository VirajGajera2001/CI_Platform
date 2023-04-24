using CI_Platform.Entities.DataModels;
using CI_Platform.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Repository
{
    public class StoryListing : IStoryListing
    {
        private readonly CIdbcontext _objdb;
        public StoryListing(CIdbcontext objdb)
        {
            _objdb = objdb;
        }
        public List<Story> stories()
        {
            List<Story> list = _objdb.Stories.Where(st => st.Status != "DRAFT").ToList();
            return list;
        }
        public List<City> cities()
        {
            List<City> cities = _objdb.Cities.ToList();
            return cities;
        }
        public List<Country> countries1()
        {
            List<Country> countries1 = _objdb.Countries.ToList();
            return countries1;
        }
        public List<MissionTheme> missionThemes()
        {
            List<MissionTheme> missionThemes = _objdb.MissionThemes.ToList();
            return missionThemes;
        }
        public StoryMedium storymedia(Story stories)
        {
            var storymedia = _objdb.StoryMedia.FirstOrDefault(sm => sm.StoryId == stories.StoryId && sm.Type=="imag");
            return storymedia;
        }
        public User users(Story stories)
        {
            var storyusername = _objdb.Users.FirstOrDefault(u => u.UserId == stories.UserId);
            return storyusername;
        }
        public Mission missions(Story stories)
        {
            var storyrelatedmis = _objdb.Missions.FirstOrDefault(ms => ms.MissionId == stories.MissionId);
            return storyrelatedmis;
        }
        public List<Mission> missions()
        {
            List<Mission> missions = _objdb.Missions.ToList();
            return missions;
        }
        public List<Mission> missions(int userId)
        {
            var missionappliedbyuser=_objdb.MissionApplications.Where(ma=>ma.UserId == userId).ToList();
            List<Mission> missions = _objdb.Missions.ToList();
            List<Mission> appliedbyuser = (from ma in missionappliedbyuser join ms in missions on ma.MissionId equals ms.MissionId select ms).ToList();
            return appliedbyuser;
        }
        public Story stories(int StoryId)
        {
            var storydetail = _objdb.Stories.FirstOrDefault(sd => sd.StoryId == StoryId);
            return storydetail;
        }
        public User users(int UserId)
        {
            var storyuser = _objdb.Users.FirstOrDefault(u => u.UserId == UserId);
            return storyuser;
        }
        public List<User> users()
        {
            List<User> users = _objdb.Users.ToList();
            return users;
        }
        public bool alreadystory(int MissionId, int UserId)
        {
            var story = _objdb.Stories.FirstOrDefault(st => st.MissionId == MissionId && st.UserId == UserId);
            if (story == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Story story(string[] Image, int MissionId, string Title, DateTime Date, string Description, int UserId, string Value, string[] videoUrls)
        {
            var story = _objdb.Stories.FirstOrDefault(st => st.MissionId == MissionId && st.UserId == UserId);
            Story storys = new Story();
            if (story == null &&Value=="save")
            {
               
                storys.Title = Title;
                storys.MissionId = MissionId;
                storys.UserId = UserId;
                storys.PublishedAt = Date;
                storys.StoryDescription = Description;
                _objdb.Stories.Add(storys);
                _objdb.SaveChanges();
                //var matchstory = _objdb.Stories.FirstOrDefault(s => s.UserId == UserId && s.MissionId == MissionId);

                foreach (var item in Image)
                {
                    StoryMedium storymedium = new StoryMedium();
                    storymedium.StoryId = storys.StoryId;
                    storymedium.Type = "imag";
                    storymedium.Path = item;
                    _objdb.StoryMedia.Add(storymedium);
                }
                foreach(var item in videoUrls)
                {
                    StoryMedium storymedium = new StoryMedium();
                    storymedium.StoryId = storys.StoryId;
                    storymedium.Type = "video";
                    storymedium.Path = item;
                    _objdb.StoryMedia.Add(storymedium);
                }
                _objdb.SaveChanges();
                return storys;
            }
           else if (story == null && Value == "submit")
            {
                //Story storys = new Story();
                storys.Title = Title;
                storys.MissionId = MissionId;
                storys.UserId = UserId;
                storys.PublishedAt = Date;
                storys.Status="pending";
                storys.StoryDescription = Description;
                _objdb.Stories.Add(storys);
                _objdb.SaveChanges();
                var matchstory = _objdb.Stories.FirstOrDefault(s => s.UserId == UserId && s.MissionId == MissionId);

                foreach (var item in Image)
                {
                    StoryMedium storymedium = new StoryMedium();
                    storymedium.StoryId = storys.StoryId;
                    storymedium.Type = "imag";
                    storymedium.Path = item;
                    _objdb.StoryMedia.Add(storymedium);
                }
                foreach (var item in videoUrls)
                {
                    StoryMedium storymedium = new StoryMedium();
                    storymedium.StoryId = storys.StoryId;
                    storymedium.Type = "video";
                    storymedium.Path = item;
                    _objdb.StoryMedia.Add(storymedium);
                }
                _objdb.SaveChanges();
                return matchstory;
            }

            else if(story!=null && Value=="save") 
            {
                story.MissionId = MissionId;
                story.UserId = UserId;
                story.Title = Title;
                story.PublishedAt = Date;
                story.StoryDescription = Description;
                _objdb.Stories.Update(story);
                List<StoryMedium> storymediums = _objdb.StoryMedia.Where(sm => sm.StoryId == story.StoryId).ToList();
                foreach (var item in storymediums)
                {
                    _objdb.StoryMedia.Remove(item);
                 
                }
                
                _objdb.SaveChanges();
                foreach (var item in Image)
                {
                    StoryMedium storymedium = new StoryMedium();
                    storymedium.StoryId = story.StoryId;
                    storymedium.Type = "imag";
                    storymedium.Path = item;
                    _objdb.StoryMedia.Add(storymedium);
                }
                foreach (var item in videoUrls)
                {
                    StoryMedium storymedium = new StoryMedium();
                    storymedium.StoryId = story.StoryId;
                    storymedium.Type = "video";
                    storymedium.Path = item;
                    _objdb.StoryMedia.Add(storymedium);
                }
                _objdb.SaveChanges();
                return story;
            }
            else
            {
                story.MissionId = MissionId;
                story.UserId = UserId;
                story.Title = Title;
                story.PublishedAt = Date;
                story.Status = "pending";
                story.StoryDescription = Description;
                _objdb.Stories.Update(story);
                List<StoryMedium> storymediums = _objdb.StoryMedia.Where(sm => sm.StoryId == story.StoryId).ToList();
                foreach (var item in storymediums)
                {
                    _objdb.StoryMedia.Remove(item);
                }
                _objdb.SaveChanges();
                foreach (var item in Image)
                {

                    StoryMedium storymedium = new StoryMedium();
                    storymedium.StoryId = story.StoryId;
                    storymedium.Type = "imag";
                    storymedium.Path = item;
                    _objdb.StoryMedia.Add(storymedium);
                }
                foreach (var item in videoUrls)
                {
                    StoryMedium storymedium = new StoryMedium();
                    storymedium.StoryId = story.StoryId;
                    storymedium.Type = "video";
                    storymedium.Path = item;
                    _objdb.StoryMedia.Add(storymedium);
                }
                _objdb.SaveChanges();
                return story;
            }
        }
        public Story searchstory(int MissionId, int UserId)
        {
            var selectstory=_objdb.Stories.Where(st=>st.MissionId==MissionId && st.UserId==UserId).FirstOrDefault();
            return selectstory;
        }
        public List<StoryMedium> searchmedias(long storyId)
        {
            var storymedium = _objdb.StoryMedia.Where(sm=>sm.StoryId==storyId).ToList();
            return storymedium;
        }
        public List<StoryMedium> storymedia(int storyid)
        {
            var media=_objdb.StoryMedia.Where(sm=>sm.StoryId== storyid).ToList();
            return media;
        }
        public void contactadd(string name, string mail, string subject, string message,int userid)
        {
            ContactU contactU=new ContactU();
            contactU.UserId=userid;
            contactU.UserName=name;
            contactU.Email=mail;
            contactU.Subject=subject;
            contactU.Message=message;
            _objdb.ContactUs.Add(contactU);
            _objdb.SaveChanges();
        }
    }
}
