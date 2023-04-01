using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Backend.DAL.Entities;

namespace Web.Backend.DAL
{
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

        public virtual DbSet<ProductInventory> ProductInventories { get; set; }

        public virtual DbSet<ProductReview> ProductReviews { get; set; }

        public virtual DbSet<PurchaseSession> PurchaseSessions { get; set; }

        public virtual DbSet<PurchasedOrder> PurchasedOrders { get; set; }

        public virtual DbSet<SystemConfig> SystemConfigs { get; set; }

        public virtual DbSet<TransactionLog> TransactionLogs { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UserAddress> UserAddresses { get; set; }

        public virtual DbSet<UserCard> UserCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActivityLog>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__ACTIVITY__3214EC075ACB0B06");

                entity.ToTable("ACTIVITY_LOG");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.CreateBy).HasMaxLength(50);
                entity.Property(e => e.CreateDate).HasMaxLength(50);
                entity.Property(e => e.ErrorCode).HasMaxLength(10);
                entity.Property(e => e.ErrorMessage).HasMaxLength(100);
            });

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__CART_ITE__3214EC07F183CCA3");

                entity.ToTable("CART_ITEM");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.CreateBy).HasMaxLength(50);
                entity.Property(e => e.CreateDate).HasColumnType("datetime");
                entity.Property(e => e.UpdateBy).HasMaxLength(50);
                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Product).WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__CART_ITEM__Produ__4AB81AF0");

                entity.HasOne(d => d.Session).WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.SessionId)
                    .HasConstraintName("FK__CART_ITEM__Sessi__49C3F6B7");
            });

            modelBuilder.Entity<DiscountCampeign>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__DISCOUNT__3214EC07F9478F31");

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
                entity.HasKey(e => e.Id).HasName("PK__DISCOUNT__3214EC076DB48179");

                entity.ToTable("DISCOUNT_COUPON");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.CreateBy).HasMaxLength(50);
                entity.Property(e => e.CreateDate).HasColumnType("datetime");
                entity.Property(e => e.ExpireDate).HasColumnType("datetime");
                entity.Property(e => e.UpdateBy).HasMaxLength(50);
                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.User).WithMany(p => p.DiscountCoupons)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__DISCOUNT___UserI__5165187F");
            });

            modelBuilder.Entity<InvoiceDetail>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__INVOICE___3214EC077ACB0D00");

                entity.ToTable("INVOICE_DETAIL");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.CreateBy).HasMaxLength(50);
                entity.Property(e => e.CreateDate).HasColumnType("datetime");
                entity.Property(e => e.UpdateBy).HasMaxLength(50);
                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__ORDER__3214EC0711281443");

                entity.ToTable("ORDER");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.CreateBy).HasMaxLength(50);
                entity.Property(e => e.CreateDate).HasColumnType("datetime");
                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 0)");
                entity.Property(e => e.UpdateBy).HasMaxLength(50);
                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Session).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.SessionId)
                    .HasConstraintName("FK__ORDER__SessionId__4BAC3F29");
            });

            modelBuilder.Entity<PaymentDetail>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__PAYMENT___3214EC07289352E8");

                entity.ToTable("PAYMENT_DETAIL");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.CreateBy).HasMaxLength(50);
                entity.Property(e => e.CreateDate).HasColumnType("datetime");
                entity.Property(e => e.UpdateBy).HasMaxLength(50);
                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Card).WithMany(p => p.PaymentDetails)
                    .HasForeignKey(d => d.CardId)
                    .HasConstraintName("FK__PAYMENT_D__CardI__52593CB8");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__PRODUCT__3214EC079520D7EF");

                entity.ToTable("PRODUCT");

                entity.Property(e => e.Id).ValueGeneratedNever();
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
                    .HasConstraintName("FK__PRODUCT__Categor__44FF419A");

                entity.HasOne(d => d.Discount).WithMany(p => p.Products)
                    .HasForeignKey(d => d.DiscountId)
                    .HasConstraintName("FK__PRODUCT__Discoun__45F365D3");

                entity.HasOne(d => d.Inventory).WithMany(p => p.Products)
                    .HasForeignKey(d => d.InventoryId)
                    .HasConstraintName("FK__PRODUCT__Invento__5441852A");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__PRODUCT___3214EC07FBF73693");

                entity.ToTable("PRODUCT_CATEGORY");

                entity.Property(e => e.Id).ValueGeneratedNever();
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

            modelBuilder.Entity<ProductInventory>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__PRODUCT___3214EC07A2EF33C2");

                entity.ToTable("PRODUCT_INVENTORY");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.CreateBy).HasMaxLength(50);
                entity.Property(e => e.CreateDate).HasColumnType("datetime");
                entity.Property(e => e.UpdateBy).HasMaxLength(50);
                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ProductReview>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__PRODUCT___3214EC07196047C9");

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
                    .HasConstraintName("FK__PRODUCT_R__Produ__5070F446");

                entity.HasOne(d => d.User).WithMany(p => p.ProductReviews)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__PRODUCT_R__UserI__534D60F1");
            });

            modelBuilder.Entity<PurchaseSession>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__PURCHASE__3214EC0747C97080");

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
                    .HasConstraintName("FK__PURCHASE___UserI__48CFD27E");
            });

            modelBuilder.Entity<PurchasedOrder>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__PURCHASE__3214EC0782713203");

                entity.ToTable("PURCHASED_ORDER");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");
                entity.Property(e => e.CreateDate).HasMaxLength(50);
                entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 0)");
                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.DiscountCoupon).WithMany(p => p.PurchasedOrders)
                    .HasForeignKey(d => d.DiscountCouponId)
                    .HasConstraintName("FK__PURCHASED__Disco__4E88ABD4");

                entity.HasOne(d => d.Invoice).WithMany(p => p.PurchasedOrders)
                    .HasForeignKey(d => d.InvoiceId)
                    .HasConstraintName("FK__PURCHASED__Invoi__4F7CD00D");

                entity.HasOne(d => d.Order).WithMany(p => p.PurchasedOrders)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__PURCHASED__Order__4CA06362");

                entity.HasOne(d => d.Payment).WithMany(p => p.PurchasedOrders)
                    .HasForeignKey(d => d.PaymentId)
                    .HasConstraintName("FK__PURCHASED__Payme__4D94879B");
            });

            modelBuilder.Entity<SystemConfig>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__SYSTEM_C__3214EC079221BA78");

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
                entity.HasKey(e => e.Id).HasName("PK__TRANSACT__3214EC07A588410C");

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
                entity.HasKey(e => e.Id).HasName("PK__USER__3214EC07CC87ACDE");

                entity.ToTable("USER");

                entity.Property(e => e.Id).ValueGeneratedNever();
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
            });

            modelBuilder.Entity<UserAddress>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__USER_ADD__3214EC0720FCD0A4");

                entity.ToTable("USER_ADDRESS");

                entity.Property(e => e.Id).ValueGeneratedNever();
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
                    .HasConstraintName("FK__USER_ADDR__UserI__46E78A0C");
            });

            modelBuilder.Entity<UserCard>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__USER_CAR__3214EC0729DCE3BB");

                entity.ToTable("USER_CARD");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.CardExpireDate).HasColumnType("datetime");
                entity.Property(e => e.CardNo).HasMaxLength(20);
                entity.Property(e => e.CreateBy).HasMaxLength(50);
                entity.Property(e => e.CreateDate).HasColumnType("datetime");
                entity.Property(e => e.NameOnCard).HasMaxLength(100);
                entity.Property(e => e.UpdateBy).HasMaxLength(50);
                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.User).WithMany(p => p.UserCards)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__USER_CARD__UserI__47DBAE45");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
