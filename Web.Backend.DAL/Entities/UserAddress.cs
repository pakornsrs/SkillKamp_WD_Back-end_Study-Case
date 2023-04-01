using System;
using System.Collections.Generic;

namespace Web.Backend.DAL.Entities;

public partial class UserAddress
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string? Province { get; set; }

    public string? District { get; set; }

    public string? Subdistrict { get; set; }

    public string? ZipCode { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public virtual User? User { get; set; }
}
