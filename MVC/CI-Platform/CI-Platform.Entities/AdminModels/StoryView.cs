using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entities.AdminModels
{
    public class StoryView
    {
        public long StoryId { get; set; }
        public long UserId { get; set; }
        public long MissionId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? StoryTitle { get; set; }
        public string? MissionTitle { get; set; }
    }
}
