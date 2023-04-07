using System;
using System.Collections.Generic;

namespace Web.Backend.DAL.Entities;

public partial class Order
{
    public int Id { get; set; }

    public int? SessionId { get; set; }

    public decimal? Amount { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? CartItem { get; set; }

    public int? CouponId { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ICollection<PurchasedOrder> PurchasedOrders { get; } = new List<PurchasedOrder>();

    public virtual PurchaseSession? Session { get; set; }
}
