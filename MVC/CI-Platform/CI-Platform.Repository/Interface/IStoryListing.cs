using CI_Platform.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CI_Platform.Repository.Interface
{
    public interface IStoryListing
    {
        public List<Story> stories();
        public List<City> cities();
        public List<Country> countries1();
        public List<MissionTheme> missionThemes();
        public StoryMedium storymedia(Story stories);
        public User users(Story stories);
        public Mission missions(Story stories);
        public List<Mission> missions(int userId);
        public List<Mission> missions();
        public Story stories(int StoryId);
        public User users(int UserId);
        public List<User> users();
        public bool alreadystory(int MissionId, int UserId);
        public Story story(string[] Image, int MissionId, string Title, DateTime Date, string Description,int UserId,string Value, string[] videoUrls);
        public Story searchstory(int MissionId, int UserId);
        public List<StoryMedium> searchmediaphoto(long storyId);
        public List<StoryMedium> searchmediavideo(long storyId);
        public List<StoryMedium> storymedia(int storyid);
        public void contactadd(string name, string mail, string subject, string message,int userid);
        public long getviewcount(int userId, int storyId);
    }
}
