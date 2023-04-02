using System;
using System.Collections.Generic;

namespace Web.Backend.DAL.Entities;

public partial class ProductInventory
{
    public int Id { get; set; }

    public int? Quantity { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ICollection<ProductDetail> ProductDetails { get; } = new List<ProductDetail>();
}
