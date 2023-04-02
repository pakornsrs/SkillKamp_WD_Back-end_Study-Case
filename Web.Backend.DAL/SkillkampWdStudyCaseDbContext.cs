using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Web.Backend.DAL.Entities;

namespace Web.Backend.DAL;

public partial class SkillkampWdStudyCaseDbContext : DbContext
{
    public SkillkampWdStudyCaseDbContext()
    {
    }

    public SkillkampWdStudyCaseDbContext(DbContextOptions<SkillkampWdStudyCaseDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivityLog> ActivityLogs { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<DiscountCampeign> DiscountCampeigns { get; set; }

    public virtual DbSet<DiscountCoupon> DiscountCoupons { get; set; }

    public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<PaymentDetail> PaymentDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductColor> ProductColors { get; set; }

    public virtual DbSet<ProductDetail> ProductDetails { get; set; }

    public virtual DbSet<ProductInventory> ProductInventories { get; set; }

    public virtual DbSet<ProductReview> ProductReviews { get; set; }

    public virtual DbSet<ProductSize> ProductSizes { get; set; }

    public virtual DbSet<PurchaseSession> PurchaseSessions { get; set; }

    public virtual DbSet<PurchasedOrder> PurchasedOrders { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SystemConfig> SystemConfigs { get; set; }

    public virtual DbSet<TransactionLog> TransactionLogs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAddress> UserAddresses { get; set; }

    public virtual DbSet<UserCard> UserCards { get; set; }

    public virtual DbSet<UserToken> UserTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Thai_CI_AS");

        modelBuilder.Entity<ActivityLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ACTIVITY__3214EC07A9D6ED9A");

            entity.ToTable("ACTIVITY_LOG");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasMaxLength(50);
            entity.Property(e => e.ErrorCode).HasMaxLength(10);
            entity.Property(e => e.ErrorMessage).HasMaxLength(100);
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CART_ITE__3214EC07ECECCE75");

            entity.ToTable("CART_ITEM");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Product).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__CART_ITEM__Produ__5441852A");

            entity.HasOne(d => d.Session).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.SessionId)
                .HasConstraintName("FK__CART_ITEM__Sessi__534D60F1");
        });

        modelBuilder.Entity<DiscountCampeign>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DISCOUNT__3214EC079076CCF8");

            entity.ToTable("DISCOUNT_CAMPEIGN");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DescEn).HasMaxLength(255);
            entity.Property(e => e.DescTh).HasMaxLength(255);
            entity.Property(e => e.DisconutPercent).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.NameEn).HasMaxLength(100);
            entity.Property(e => e.NameTh)
                .HasMaxLength(100)
                .HasColumnName("NameTH");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<DiscountCoupon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DISCOUNT__3214EC070CB077BD");

            entity.ToTable("DISCOUNT_COUPON");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.ExpireDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.DiscountCoupons)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__DISCOUNT___UserI__5AEE82B9");
        });

        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__INVOICE___3214EC0707DC4FD1");

            entity.ToTable("INVOICE_DETAIL");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ORDER__3214EC074267D131");

            entity.ToTable("ORDER");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Session).WithMany(p => p.Orders)
                .HasForeignKey(d => d.SessionId)
                .HasConstraintName("FK__ORDER__SessionId__5535A963");
        });

        modelBuilder.Entity<PaymentDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PAYMENT___3214EC07E34482CE");

            entity.ToTable("PAYMENT_DETAIL");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Card).WithMany(p => p.PaymentDetails)
                .HasForeignKey(d => d.CardId)
                .HasConstraintName("FK__PAYMENT_D__CardI__5BE2A6F2");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PRODUCT__3214EC075EA19960");

            entity.ToTable("PRODUCT");

            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DescEn).HasMaxLength(255);
            entity.Property(e => e.DescTh).HasMaxLength(255);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ProductNameEn).HasMaxLength(50);
            entity.Property(e => e.ProductNameTh).HasMaxLength(50);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__PRODUCT__Categor__4E88ABD4");

            entity.HasOne(d => d.Discount).WithMany(p => p.Products)
                .HasForeignKey(d => d.DiscountId)
                .HasConstraintName("FK__PRODUCT__Discoun__4F7CD00D");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PRODUCT___3214EC07742F0371");

            entity.ToTable("PRODUCT_CATEGORY");

            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DescEn).HasMaxLength(255);
            entity.Property(e => e.DescTh).HasMaxLength(255);
            entity.Property(e => e.NameEn).HasMaxLength(50);
            entity.Property(e => e.NameTh)
                .HasMaxLength(50)
                .HasColumnName("NameTH");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ProductColor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PRODUCT___3214EC07DF27EA5B");

            entity.ToTable("PRODUCT_COLOR");

            entity.Property(e => e.ColorCode).HasMaxLength(20);
            entity.Property(e => e.ColorCodeRgb)
                .HasMaxLength(20)
                .HasColumnName("ColorCodeRGB");
            entity.Property(e => e.ColorNameEn).HasMaxLength(20);
            entity.Property(e => e.ColorNameTh).HasMaxLength(20);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ProductDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PRODUCT___3214EC07D42F7B6A");

            entity.ToTable("PRODUCT_DETAIL");

            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Color).WithMany(p => p.ProductDetails)
                .HasForeignKey(d => d.ColorId)
                .HasConstraintName("FK__PRODUCT_D__Color__60A75C0F");

            entity.HasOne(d => d.Inventory).WithMany(p => p.ProductDetails)
                .HasForeignKey(d => d.InventoryId)
                .HasConstraintName("FK__PRODUCT_D__Inven__628FA481");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__PRODUCT_D__Produ__5FB337D6");

            entity.HasOne(d => d.Size).WithMany(p => p.ProductDetails)
                .HasForeignKey(d => d.SizeId)
                .HasConstraintName("FK__PRODUCT_D__SizeI__619B8048");
        });

        modelBuilder.Entity<ProductInventory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PRODUCT___3214EC077831E439");

            entity.ToTable("PRODUCT_INVENTORY");

            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ProductReview>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PRODUCT___3214EC07C989B80C");

            entity.ToTable("PRODUCT_REVIEW");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.ReviewerName).HasMaxLength(50);
            entity.Property(e => e.ReviewerText).HasMaxLength(255);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductReviews)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__PRODUCT_R__Produ__59FA5E80");

            entity.HasOne(d => d.User).WithMany(p => p.ProductReviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__PRODUCT_R__UserI__5CD6CB2B");
        });

        modelBuilder.Entity<ProductSize>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PRODUCT___3214EC075D7B2EB0");

            entity.ToTable("PRODUCT_SIZE");

            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.SizeDescEn).HasMaxLength(20);
            entity.Property(e => e.SizeDescTh).HasMaxLength(20);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<PurchaseSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PURCHASE__3214EC07324F21A0");

            entity.ToTable("PURCHASE_SESSION");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Expire).HasColumnType("datetime");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.PurchaseSessions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__PURCHASE___UserI__52593CB8");
        });

        modelBuilder.Entity<PurchasedOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PURCHASE__3214EC072F6F4724");

            entity.ToTable("PURCHASED_ORDER");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.CreateDate).HasMaxLength(50);
            entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.DiscountCoupon).WithMany(p => p.PurchasedOrders)
                .HasForeignKey(d => d.DiscountCouponId)
                .HasConstraintName("FK__PURCHASED__Disco__5812160E");

            entity.HasOne(d => d.Invoice).WithMany(p => p.PurchasedOrders)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("FK__PURCHASED__Invoi__59063A47");

            entity.HasOne(d => d.Order).WithMany(p => p.PurchasedOrders)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__PURCHASED__Order__5629CD9C");

            entity.HasOne(d => d.Payment).WithMany(p => p.PurchasedOrders)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK__PURCHASED__Payme__571DF1D5");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ROLE__3214EC07E3F7FCF1");

            entity.ToTable("ROLE");

            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.RoleNameEn).HasMaxLength(20);
            entity.Property(e => e.RoleNameTh)
                .HasMaxLength(20)
                .HasColumnName("RoleNameTH");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<SystemConfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SYSTEM_C__3214EC073136E90E");

            entity.ToTable("SYSTEM_CONFIG");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ConfigDescEn).HasMaxLength(255);
            entity.Property(e => e.ConfigDescTh).HasMaxLength(255);
            entity.Property(e => e.ConfigName).HasMaxLength(100);
            entity.Property(e => e.ConfigValue).HasMaxLength(50);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TransactionLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TRANSACT__3214EC07CC5BEEF2");

            entity.ToTable("TRANSACTION_LOG");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.ErrorCode).HasMaxLength(10);
            entity.Property(e => e.ErrorMessage).HasMaxLength(100);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USER__3214EC0761EB1029");

            entity.ToTable("USER");

            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstNameEn).HasMaxLength(100);
            entity.Property(e => e.FirstNameTh).HasMaxLength(100);
            entity.Property(e => e.LastNameEn).HasMaxLength(100);
            entity.Property(e => e.LastNameTh).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.TelNo).HasMaxLength(10);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(20);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__USER__RoleId__5DCAEF64");

            entity.HasOne(d => d.UserToken).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserTokenId)
                .HasConstraintName("FK__USER__UserTokenI__5EBF139D");
        });

        modelBuilder.Entity<UserAddress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USER_ADD__3214EC0760795316");

            entity.ToTable("USER_ADDRESS");

            entity.Property(e => e.AddressLine1).HasMaxLength(255);
            entity.Property(e => e.AddressLine2).HasMaxLength(255);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.District).HasMaxLength(100);
            entity.Property(e => e.Province).HasMaxLength(100);
            entity.Property(e => e.Subdistrict).HasMaxLength(100);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.ZipCode).HasMaxLength(10);

            entity.HasOne(d => d.User).WithMany(p => p.UserAddresses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__USER_ADDR__UserI__5070F446");
        });

        modelBuilder.Entity<UserCard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USER_CAR__3214EC0763B25E38");

            entity.ToTable("USER_CARD");

            entity.Property(e => e.CardExpireDate).HasColumnType("datetime");
            entity.Property(e => e.CardNo).HasMaxLength(20);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.NameOnCard).HasMaxLength(100);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.UserCards)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__USER_CARD__UserI__5165187F");
        });

        modelBuilder.Entity<UserToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USER_TOK__3214EC0760176084");

            entity.ToTable("USER_TOKEN");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Expire).HasColumnType("datetime");
            entity.Property(e => e.Token).HasMaxLength(255);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
