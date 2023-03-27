using CI_Platform.Entities.DataModels;

namespace CI_Platform.Models
{
    public class StoryViewModel
    {
        public long StoryId { get; set; }

        public long UserId { get; set; }

        public long MissionId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string Status { get; set; } = null!;

        public DateTime? PublishedAt { get; set; }

        public byte[] CreatedAt { get; set; } = null!;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }
        public string? Avatar { get; set; }
        public string Path { get; set; } = null!;
        public long CityId { get; set; }

        public long CountryId { get; set; }
        public string ThemeTitle { get; set; } = null!;
        public string StoryDescription { get; set; }
        
    }
}
