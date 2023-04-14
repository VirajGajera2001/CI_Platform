using CI_Platform.Entities.DataModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entities.Models
{
    public class UserView
    {
        public long UserId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public int PhoneNumber { get; set; }

        public string? Avatar { get; set; }

        public string? WhyIVolunteer { get; set; }

        public string? EmployeeId { get; set; }

        public string? Department { get; set; }
        public string? ManagerDetail { get; set; }

        public long? CityId { get; set; }

        public long? CountryId { get; set; }

        public string? ProfileText { get; set; }

        public string? LinkedInUrl { get; set; }

        public string? Title { get; set; }
        public string? Availability { get; set; }

        public bool? Status { get; set; }

        public byte[] CreatedAt { get; set; } = null!;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }


        public long SkillId { get; set; }

        public string SkillName { get; set; } = null!;
        public List<SelectListItem> cities { get; set; } = null!;
        public List<SelectListItem> countries { get; set; } = null!;
        public List<SelectListItem> skills { get; set; } = null!;
        public List<SelectListItem> userskill { get; set; } = null!;
    }
}
