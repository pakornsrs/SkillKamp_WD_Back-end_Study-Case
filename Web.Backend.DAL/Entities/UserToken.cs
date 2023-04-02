using System;
using System.Collections.Generic;

namespace Web.Backend.DAL.Entities;

public partial class UserToken
{
    public int Id { get; set; }

    public string? Token { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? Expire { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ICollection<User> Users { get; } = new List<User>();
}
