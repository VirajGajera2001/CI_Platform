using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entities.AdminModels
{
    public class MissionApplicationView
    {
        public long MissionApplicationId { get; set; }
        public string Title { get; set; } = null!;
        public long MissionId { get; set; }
        public long UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime AppliedAt { get; set; }
    }
}
