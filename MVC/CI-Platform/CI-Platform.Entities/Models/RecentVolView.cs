using System.Collections;
using System.Collections.Generic;
using CI_Platform.Entities.DataModels;

namespace CI_Platform.Entities.Models
{
    public class RecentVolView : IEnumerable<User>
    {
        public List<User> recentuser = new List<User>();

        public IEnumerator<User> GetEnumerator()
        {
            return recentuser.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
