﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CI_Platform.Entities.DataModels;

public partial class User
{
    public long UserId { get; set; }
    [Required(ErrorMessage = "FirstName is required")]
    public string? FirstName { get; set; }
    [Required(ErrorMessage = "LastName is required")]
    public string? LastName { get; set; }
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = null!;
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = null!;
    [Required(ErrorMessage = "PhoneNumber is required")]
    public string PhoneNumber { get; set; } = null!;

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

    public string? Role { get; set; }

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<ContactU> ContactUs { get; } = new List<ContactU>();

    public virtual ICollection<FavouriteMission> FavouriteMissions { get; } = new List<FavouriteMission>();

    public virtual ICollection<MissionApplication> MissionApplications { get; } = new List<MissionApplication>();

    public virtual ICollection<MissionInvite> MissionInviteFromUsers { get; } = new List<MissionInvite>();

    public virtual ICollection<MissionInvite> MissionInviteToUsers { get; } = new List<MissionInvite>();

    public virtual ICollection<MissionRating> MissionRatings { get; } = new List<MissionRating>();

    public virtual ICollection<Story> Stories { get; } = new List<Story>();

    public virtual ICollection<Timesheet> Timesheets { get; } = new List<Timesheet>();

    public virtual ICollection<UserSkill> UserSkills { get; } = new List<UserSkill>();
}
