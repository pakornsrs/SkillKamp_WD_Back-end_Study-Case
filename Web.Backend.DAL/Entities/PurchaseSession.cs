using System;
using System.Collections.Generic;

namespace Web.Backend.DAL.Entities;

public partial class PurchaseSession
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? Expire { get; set; }

    public decimal? TotalPrice { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ICollection<CartItem> CartItems { get; } = new List<CartItem>();

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual User? User { get; set; }
}
