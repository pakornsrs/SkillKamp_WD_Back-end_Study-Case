using System;
using System.Collections.Generic;

namespace Web.Backend.DAL.Entities;

public partial class PaymentDetail
{
    public int Id { get; set; }

    public int? PaymentMethod { get; set; }

    public int? CardId { get; set; }

    public int? Status { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public virtual UserCard? Card { get; set; }

    public virtual ICollection<PurchasedOrder> PurchasedOrders { get; } = new List<PurchasedOrder>();
}
