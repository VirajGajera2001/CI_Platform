using System;
using System.Collections.Generic;

namespace CI_Platform.DataModels;

public partial class Banner
{
    public long BannerId { get; set; }

    public string Image { get; set; } = null!;

    public string Text { get; set; } = null!;

    public int SortOrder { get; set; }

    public byte[] CreatedAt { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
