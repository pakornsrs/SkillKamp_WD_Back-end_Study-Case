using System;
using System.Collections.Generic;

namespace Web.Backend.DAL.Entities;

public partial class ProductReview
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? ProductId { get; set; }

    public decimal Rating { get; set; }

    public string? ReviewerName { get; set; }

    public string? ReviewerText { get; set; }

    public bool? IsRecomment { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? User { get; set; }
}
