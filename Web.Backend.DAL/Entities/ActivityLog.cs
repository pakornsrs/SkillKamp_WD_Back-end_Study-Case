using System;
using System.Collections.Generic;

namespace Web.Backend.DAL.Entities;

public partial class ActivityLog
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? Activity { get; set; }

    public string? ActivityDesc { get; set; }

    public int? Status { get; set; }

    public string? ErrorCode { get; set; }

    public string? ErrorMessage { get; set; }

    public string? ExceptionMessage { get; set; }

    public string? CreateDate { get; set; }

    public string? CreateBy { get; set; }
}
