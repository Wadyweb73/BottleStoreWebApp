using Microsoft.EntityFrameworkCore;
using BottleStoreWebApp.Models;

namespace BottleStoreWebApp.Data
{
    public class BottleStoreDbContext : DbContext
    {
        public BottleStoreDbContext(DbContextOptions<BottleStoreDbContext> options) : base(options) 
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductInformation> ProductInformations { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleReceipt> SaleReceipts { get; set; }
        public DbSet<UserApprovalRequest> UserApprovalRequests { get; set; }
        public DbSet<InventoryLog> InventoryLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // USERS
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(100);
                entity.Property(e => e.PhoneNumber).HasColumnName("phone_number").HasMaxLength(20);
                entity.Property(e => e.EmailAddress).HasColumnName("email_address").IsRequired().HasMaxLength(100);
                entity.Property(e => e.Password).HasColumnName("password").IsRequired().HasMaxLength(255);
                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UserType).HasColumnName("user_type").IsRequired();
                entity.Property(e => e.IsApproved).HasColumnName("is_approved").HasDefaultValue(false);
                entity.Property(e => e.IsActive).HasColumnName("is_active").HasDefaultValue(true);
            });

            // PRODUCTS
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ProductName).HasColumnName("product_name").IsRequired().HasMaxLength(200);
                entity.Property(e => e.ProductCategory).HasColumnName("product_category").IsRequired().HasMaxLength(100);
                entity.Property(e => e.ProductSize).HasColumnName("product_size").HasMaxLength(50);
                entity.Property(e => e.IsActive).HasColumnName("is_active").HasDefaultValue(true);
            });

            // PRODUCT_INFORMATION
            modelBuilder.Entity<ProductInformation>(entity =>
            {
                entity.ToTable("product_information");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ProductId).HasColumnName("product_id").IsRequired();
                entity.Property(e => e.AmountBoxes).HasColumnName("amount_boxes").HasDefaultValue(0);
                entity.Property(e => e.PurchasePrice).HasColumnName("purchase_price").HasColumnType("decimal(10,2)").IsRequired();
                entity.Property(e => e.UnitPrice).HasColumnName("unit_price").HasColumnType("decimal(10,2)").IsRequired();
                entity.Property(e => e.RegisterDate).HasColumnName("register_date").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.ProductSupplier).HasColumnName("product_supplier").HasMaxLength(200);

                entity.HasOne(pi => pi.Product)
                      .WithMany(p => p.ProductInformations)
                      .HasForeignKey(pi => pi.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // SALES
            modelBuilder.Entity<Sale>(entity =>
            {
                entity.ToTable("sales");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ProductId).HasColumnName("product_id").IsRequired();
                entity.Property(e => e.UserId).HasColumnName("user_id").IsRequired();
                entity.Property(e => e.Amount).HasColumnName("amount").IsRequired();
                entity.Property(e => e.UnitPrice).HasColumnName("unit_price").HasColumnType("decimal(10,2)").IsRequired();
                entity.Property(e => e.TotalAmount).HasColumnName("total_amount").HasColumnType("decimal(10,2)").IsRequired();
                entity.Property(e => e.SaleDate).HasColumnName("sale_date").HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(s => s.Product)
                      .WithMany(p => p.Sales)
                      .HasForeignKey(s => s.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.User)
                      .WithMany(u => u.Sales)
                      .HasForeignKey(s => s.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // SALE_RECEIPTS
            modelBuilder.Entity<SaleReceipt>(entity =>
            {
                entity.ToTable("sale_receipts");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.SaleId).HasColumnName("sale_id").IsRequired();
                entity.Property(e => e.SaleDate).HasColumnName("sale_date");
                entity.Property(e => e.Amount).HasColumnName("amount").HasColumnType("decimal(10,2)").IsRequired();
                entity.Property(e => e.CustomerName).HasColumnName("customer_name").HasMaxLength(200);
                entity.Property(e => e.CustomerPhone).HasColumnName("customer_phone").HasMaxLength(20);
                entity.Property(e => e.PaymentMethod).HasColumnName("payment_method").HasMaxLength(50);

                entity.HasOne(sr => sr.Sale)
                      .WithOne(s => s.SaleReceipt)
                      .HasForeignKey<SaleReceipt>(sr => sr.SaleId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            /* USER_APPROVAL_REQUESTS
            modelBuilder.Entity<UserApprovalRequest>(entity =>
            {
                entity.ToTable("user_approval_requests");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.RequestDate).HasColumnName("request_date").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.ApprovedBy).HasColumnName("approved_by");
                entity.Property(e => e.ApprovalDate).HasColumnName("approval_date");
                entity.Property(e => e.Status).HasColumnName("status").HasDefaultValue(ApprovalStatus.Pendente);
                entity.Property(e => e.Comments).HasColumnName("comments").HasColumnType("TEXT");

                entity.HasOne(u => u.User)
                      .WithMany(u => u.ApprovalRequests)
                      .HasForeignKey(u => u.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(u => u.ApprovedByUser)
                      .WithMany()
                      .HasForeignKey(u => u.ApprovedBy)
                      .OnDelete(DeleteBehavior.SetNull);
            });
            */

            // USER_APPROVAL_REQUESTS
            modelBuilder.Entity<UserApprovalRequest>(entity =>
            {
                entity.ToTable("user_approval_requests");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.RequestDate).HasColumnName("request_date").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.ApprovedBy).HasColumnName("approved_by");
                entity.Property(e => e.ApprovalDate).HasColumnName("approval_date");
                entity.Property(e => e.Status).HasColumnName("status").HasDefaultValue(ApprovalStatus.Pendente);
                entity.Property(e => e.Comments).HasColumnName("comments").HasColumnType("TEXT");

                entity.HasOne(r => r.User)
                      .WithMany(u => u.ApprovalRequests)
                      .HasForeignKey(r => r.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.ApprovedByUser)
                      .WithMany(u => u.ApprovedRequests)
                      .HasForeignKey(r => r.ApprovedBy)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // INVENTORY_LOGS
            modelBuilder.Entity<InventoryLog>(entity =>
            {
                entity.ToTable("inventory_logs");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.MovementType).HasColumnName("movement_type");
                entity.Property(e => e.Quantity).HasColumnName("quantity");
                entity.Property(e => e.PreviousStock).HasColumnName("previous_stock");
                entity.Property(e => e.NewStock).HasColumnName("new_stock");
                entity.Property(e => e.Reason).HasColumnName("reason").HasColumnType("TEXT");
                entity.Property(e => e.MovementDate).HasColumnName("movement_date").HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(i => i.Product)
                      .WithMany(p => p.InventoryLogs)
                      .HasForeignKey(i => i.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(i => i.User)
                      .WithMany(u => u.InventoryLogs)
                      .HasForeignKey(i => i.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
