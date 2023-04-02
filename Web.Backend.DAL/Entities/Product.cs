using System;
using System.Collections.Generic;

namespace Web.Backend.DAL.Entities;

public partial class Product
{
    public int Id { get; set; }

    public int? CategoryId { get; set; }

    public int? ProductDetailId { get; set; }

    public string? ProductNameTh { get; set; }

    public string? ProductNameEn { get; set; }

    public string? DescTh { get; set; }

    public string? DescEn { get; set; }

    public decimal? Price { get; set; }

    public bool? CanUseDiscountCode { get; set; }

    public bool? IsDiscount { get; set; }

    public int? DiscountId { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ICollection<CartItem> CartItems { get; } = new List<CartItem>();

    public virtual ProductCategory? Category { get; set; }

    public virtual DiscountCampeign? Discount { get; set; }

    public virtual ICollection<ProductDetail> ProductDetails { get; } = new List<ProductDetail>();

    public virtual ICollection<ProductReview> ProductReviews { get; } = new List<ProductReview>();
}
