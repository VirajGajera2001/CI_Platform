using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entities.Models
{
    public class SkillDataModel
    {
        public long SkillId { get; set; }

        public string SkillName { get; set; } = null!;

        public byte Status { get; set; }

        public byte[] CreatedAt { get; set; } = null!;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
