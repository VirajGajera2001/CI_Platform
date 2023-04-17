using CI_Platform.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entities.AdminModels
{
    public class BannerView
    {
        public List<Banner> banners = new List<Banner>();
        public long BannerId { get; set; }

        public string Image { get; set; } = null!;

        public string Text { get; set; } = null!;

        public int SortOrder { get; set; }
    }
}
