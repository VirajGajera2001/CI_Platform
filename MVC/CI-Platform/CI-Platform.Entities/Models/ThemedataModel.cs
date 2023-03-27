using CI_Platform.Entities.DataModels;

namespace CI_Platform.Models
{
    public class ThemedataModel
    {
        public long MissionThemeId { get; set; }

        public string Title { get; set; } = null!;

        public byte Status { get; set; }

        public byte[] CreatedAt { get; set; } = null!;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
