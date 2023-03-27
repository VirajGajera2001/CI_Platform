using CI_Platform.Entities.DataModels;
using CI_Platform.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Repository
{
    public class StoryListing: IStoryListing
    {
        private readonly CIdbcontext _objdb;
        public StoryListing(CIdbcontext objdb) 
        {
            _objdb=objdb;
        }
        public List<Story> stories()
        {
            List<Story> list = _objdb.Stories.ToList();
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
            var storymedia = _objdb.StoryMedia.FirstOrDefault(sm => sm.StoryId == stories.StoryId);
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
            List<Mission> missions= _objdb.Missions.ToList();
            return missions;
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
            List<User> users= _objdb.Users.ToList();
            return users;
        }
        public bool alreadystory(int MissionId, int UserId)
        {
            var story=_objdb.Stories.FirstOrDefault(st=>st.MissionId== MissionId&&st.UserId==UserId);
            if (story == null) {
                return true;
            } 
            else { 
                return false;
            }
        }
        public void story(string[] Image, int MissionId, string Title, DateTime Date, string Description,int UserId)
        {
            Story story= new Story();
            story.Title = Title;
            story.MissionId= MissionId;
            story.UserId= UserId;
            story.PublishedAt = Date;
            story.StoryDescription= Description;
            _objdb.Stories.Add(story);
            _objdb.SaveChanges();
            var matchstory = _objdb.Stories.FirstOrDefault(s => s.UserId == UserId &&s.MissionId==MissionId);


            foreach(var item in Image)
            {
                StoryMedium storymedium = new StoryMedium();
                storymedium.StoryId = matchstory.StoryId;
                storymedium.Path = item;
                _objdb.StoryMedia.Add(storymedium);
                _objdb.SaveChanges();
            }
        }
    }
}
