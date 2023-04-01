using System;
using System.Collections.Generic;

namespace Web.Backend.DAL.Entities;

public partial class ActivityLog
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? Activity { get; set; }

    public int? ActivityDesc { get; set; }

    public int? Status { get; set; }

    public string? ErrorCode { get; set; }

    public string? ErrorMessage { get; set; }

    public string? CreateDate { get; set; }

    public string? CreateBy { get; set; }
}
