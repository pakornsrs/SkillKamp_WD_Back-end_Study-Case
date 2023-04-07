using System;
using System.Collections.Generic;

namespace Web.Backend.DAL.Entities;

public partial class DiscountCoupon
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? CouponCode { get; set; }

    public int? Type { get; set; }

    public decimal? PercentDiscount { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ExpireDate { get; set; }

    public DateTime? UseDate { get; set; }

    public int? UseCount { get; set; }

    public int? Limitation { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ICollection<PurchasedOrder> PurchasedOrders { get; } = new List<PurchasedOrder>();

    public virtual User? User { get; set; }
}
