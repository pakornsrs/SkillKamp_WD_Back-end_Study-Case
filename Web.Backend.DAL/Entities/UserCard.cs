using System;
using System.Collections.Generic;

namespace Web.Backend.DAL.Entities;

public partial class UserCard
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? CardNo { get; set; }

    public string? NameOnCard { get; set; }

    public DateTime? CardExpireDate { get; set; }

    public int? Provider { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ICollection<PaymentDetail> PaymentDetails { get; } = new List<PaymentDetail>();

    public virtual User? User { get; set; }
}
