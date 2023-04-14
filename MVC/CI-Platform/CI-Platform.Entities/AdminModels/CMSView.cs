using CI_Platform.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entities.AdminModels
{
    public class CMSView
    {
        public List<CmsPage> CmsPages { get; set; }
        public long CmsPageId { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Slug { get; set; } = null!;

        public string? Status { get; set; }
    }
}
