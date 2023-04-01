using System;
using System.Collections.Generic;

namespace Web.Backend.DAL.Entities;

public partial class TransactionLog
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? PurchaseId { get; set; }

    public int? InvoiceId { get; set; }

    public string? Product { get; set; }

    public decimal? TotalAmount { get; set; }

    public int? TransactionStatus { get; set; }

    public string? ErrorCode { get; set; }

    public string? ErrorMessage { get; set; }

    public int? PaymentMethod { get; set; }

    public int? PaymentStatus { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }
}
