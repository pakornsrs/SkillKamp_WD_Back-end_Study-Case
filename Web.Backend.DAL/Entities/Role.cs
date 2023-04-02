using System;
using System.Collections.Generic;

namespace Web.Backend.DAL.Entities;

public partial class Role
{
    public int Id { get; set; }

    public string? RoleNameTh { get; set; }

    public string? RoleNameEn { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ICollection<User> Users { get; } = new List<User>();
}
