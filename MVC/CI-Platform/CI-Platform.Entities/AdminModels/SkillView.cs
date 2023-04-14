using CI_Platform.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entities.AdminModels
{
    public class SkillView
    {
        public List<Skill> skills= new List<Skill>();
        public long SkillId { get; set; }

        public string SkillName { get; set; } = null!;

        public string Status { get; set; }
    }
}
