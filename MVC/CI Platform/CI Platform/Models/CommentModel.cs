using CI_Platform.DataModels;

namespace CI_Platform.Models
{
    public class CommentModel
    {
        public long CommentId { get; set; }

        public long UserId { get; set; }

        public long MissionId { get; set; }

        public string CommentText { get; set; } = null!;

        public string ApprovalStatus { get; set; } = null!;

        public byte[] CreatedAt { get; set; } = null!;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? FirstName { get; set; }
    }
}
