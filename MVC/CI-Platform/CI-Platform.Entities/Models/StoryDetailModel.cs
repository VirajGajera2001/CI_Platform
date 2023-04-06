using CI_Platform.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entities.Models
{
    public class StoryDetailModel
    {
        public List<User> Allusers= new List<User>();
        public Story story = new Story();
        public User storyuser= new User();
        public List<StoryMedium> storyMedia = new List<StoryMedium>();
    }
}
