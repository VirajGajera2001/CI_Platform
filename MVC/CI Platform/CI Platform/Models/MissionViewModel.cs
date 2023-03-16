using CI_Platform.DataModels;
using System.ComponentModel.DataAnnotations;

namespace CI_Platform.Models
{
    public class MissionViewModel
    {
        public long MissionId { get; set; }

        public long ThemeId { get; set; }

        public long CityId { get; set; }

        public long CountryId { get; set; }

        public string Title { get; set; } = null!;

        public string ShortDescription { get; set; } = null!;

        public string? Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string MissionType { get; set; } = null!;

        public bool Status { get; set; }

        public string? OrganizationName { get; set; }

        public string? OrganizationDetalis { get; set; }

        public string? Availability { get; set; }

        public byte[] CreatedAt { get; set; } = null!;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public  String City { get; set; } = null!;

        public  String Country { get; set; } = null!;

        public bool isFav { get; set; }
        public String Theme { get; set; } = null!;
        public int? SeatsAvailable { get; set; }=null!;
        public string? MediaPath { get; set; } = null;
        public int? Rating { get; set; }
        public string? FirstName { get; set; }
        public string Email { get; set; } = null!;
        public int? GoalValue { get; set; }
    }
}
