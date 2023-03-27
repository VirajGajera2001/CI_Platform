using CI_Platform.Entities.DataModels;

namespace CI_Platform.Models
{
    public class CommentModel
    {
        public long CommentId { get; set; }

        public long UserId { get; set; }

        public long MissionId { get; set; }

        public string CommentText { get; set; } = null!;

        public string ApprovalStatus { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? FirstName { get; set; }
        public string? Avatar { get; set; }
    }
}
