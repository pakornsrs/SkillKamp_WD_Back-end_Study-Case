using System;
using System.Collections.Generic;

namespace Web.Backend.DAL.Entities;

public partial class PurchasedOrder
{
    public int Id { get; set; }

    public int? PaymentId { get; set; }

    public int? DiscountCouponId { get; set; }

    public int? InvoiceId { get; set; }

    public int? OrderId { get; set; }

    public decimal? Amount { get; set; }

    public decimal? DiscountAmount { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? CreateDate { get; set; }

    public virtual DiscountCoupon? DiscountCoupon { get; set; }

    public virtual InvoiceDetail? Invoice { get; set; }

    public virtual Order? Order { get; set; }

    public virtual PaymentDetail? Payment { get; set; }
}
