using System;
using System.Collections.Generic;

namespace Web.Backend.DAL.Entities;

public partial class CartItem
{
    public int Id { get; set; }

    public int? SessionId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public virtual Product? Product { get; set; }

    public virtual PurchaseSession? Session { get; set; }
}
