using System;
using System.Collections.Generic;

namespace Web.Backend.DAL.Entities;

public partial class ProductColor
{
    public int Id { get; set; }

    public string? ColorNameTh { get; set; }

    public string? ColorNameEn { get; set; }

    public string? ColorCode { get; set; }

    public string? ColorCodeRgb { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ICollection<ProductDetail> ProductDetails { get; } = new List<ProductDetail>();
}
