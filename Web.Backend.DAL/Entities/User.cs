using System;
using System.Collections.Generic;

namespace Web.Backend.DAL.Entities;

public partial class User
{
    public int Id { get; set; }

    public int? UserTokenId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? FirstNameTh { get; set; }

    public string? LastNameTh { get; set; }

    public string? FirstNameEn { get; set; }

    public string? LastNameEn { get; set; }

    public DateTime? BirthDate { get; set; }

    public int? Gender { get; set; }

    public string? TelNo { get; set; }

    public string? Email { get; set; }

    public int? RoleId { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ICollection<DiscountCoupon> DiscountCoupons { get; } = new List<DiscountCoupon>();

    public virtual ICollection<ProductReview> ProductReviews { get; } = new List<ProductReview>();

    public virtual ICollection<PurchaseSession> PurchaseSessions { get; } = new List<PurchaseSession>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<UserAddress> UserAddresses { get; } = new List<UserAddress>();

    public virtual ICollection<UserCard> UserCards { get; } = new List<UserCard>();

    public virtual UserToken? UserToken { get; set; }
}
