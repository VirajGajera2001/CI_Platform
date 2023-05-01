using System;
using System.Collections.Generic;

namespace CI_Platform.Entities.DataModels;

public partial class StoryView
{
    public long ViewId { get; set; }

    public long StoryId { get; set; }

    public long UserId { get; set; }
}
