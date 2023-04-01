using System;
using System.Collections.Generic;

namespace Web.Backend.DAL.Entities;

public partial class SystemConfig
{
    public int Id { get; set; }

    public string? ConfigName { get; set; }

    public string? ConfigDescTh { get; set; }

    public string? ConfigDescEn { get; set; }

    public string? ConfigValue { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }
}
