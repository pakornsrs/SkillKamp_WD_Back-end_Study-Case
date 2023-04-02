using System;
using System.Collections.Generic;

namespace Web.Backend.DAL.Entities;

public partial class ProductDetail
{
    public int Id { get; set; }

    public int? InventoryId { get; set; }

    public int? ProductId { get; set; }

    public decimal? Price { get; set; }

    public bool? IsActive { get; set; }

    public int? SizeId { get; set; }

    public int? ColorId { get; set; }

    public string? ImagePath { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ProductColor? Color { get; set; }

    public virtual ProductInventory? Inventory { get; set; }

    public virtual Product? Product { get; set; }

    public virtual ProductSize? Size { get; set; }
}
