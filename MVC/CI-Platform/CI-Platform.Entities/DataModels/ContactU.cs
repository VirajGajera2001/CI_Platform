﻿using System;
using System.Collections.Generic;

namespace CI_Platform.Entities.DataModels;

public partial class ContactU
{
    public long ContactId { get; set; }

    public long UserId { get; set; }

    public string? UserName { get; set; }

    public string Email { get; set; } = null!;

    public string? Subject { get; set; }

    public string? Message { get; set; }

    public virtual User User { get; set; } = null!;
}
