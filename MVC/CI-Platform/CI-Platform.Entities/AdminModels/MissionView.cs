using CI_Platform.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entities.AdminModels
{
    public class MissionView
    {
        public List<Mission> missions = new List<Mission>();
        public List<MissionTheme> missionThemes= new List<MissionTheme>();
        public List<Country> countries= new List<Country>();
        public List<Skill> skills= new List<Skill>();
        public List<MissionMedium> missionMedia= new List<MissionMedium>();
        public List<MissionDocument> missionDocuments= new List<MissionDocument>();
        public List<MissionSkill> missionSkills= new List<MissionSkill>();
        public long MissionId { get; set; }

        public long ThemeId { get; set; }

        public long CityId { get; set; }

        public long CountryId { get; set; }

        public string Title { get; set; } = null!;

        public string ShortDescription { get; set; } = null!;

        public string? Description { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public string MissionType { get; set; } = null!;

        public bool Status { get; set; }

        public string? OrganizationName { get; set; }

        public string? OrganizationDetalis { get; set; }

        public string? Availability { get; set; }
        public int? SeatsAvailable { get; set; }
        public DateTime? Deadline { get; set; }
        public long[] SkillId { get; set; } = null!;
        public string? GoalObjectiveText { get; set; }

        public int GoalValue { get; set; }
    }
}
