using System;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.MobileAppService.Models
{
    public partial class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {
            //this.Database = s => System.Diagnostics.Debug.WriteLine(s);

        }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<SerialNumber> SerialNumbers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:inventory-manager.database.windows.net,1433;Initial Catalog=InventoryManager;Persist Security Info=False;User ID=Nabil;Password=Lynn!58426;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId })
                    .HasName("PK__order_it__08D097A3EB33D61F");

                entity.ToTable("OrderItems", "sales");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__order_ite__Order__123EB7A3");
            });
            

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__orders__4659622945ED217A");

                entity.ToTable("Orders", "sales");

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__orders__store_id__693CA210");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__orders__staff_id__59063A47");
            });

            //modelBuilder.Entity<OrderItems>(entity =>
            //{
            //    entity.HasKey(e => new { e.OrderId, e.ProductId })
            //        .HasName("PK__order_it__837942D4C00A9444");

            //    entity.ToTable("OrderItems", "sales");

            //    entity.Property(e => e.OrderId).HasColumnName("OrderId");

            //    entity.Property(e => e.ProductId).HasColumnName("ProductId");

            //    //entity.Property(e => e.ListPrice)
            //    //    .HasColumnName("list_price")
            //    //    .HasColumnType("decimal(10, 2)");

            //    //entity.Property(e => e.ProductId).HasColumnName("product_id");

            //    entity.Property(e => e.Quantity).HasColumnName("Quantity");

            //    //entity.HasOne(d => d.Order)
            //    //    .WithMany(p => p.OrderItems)
            //    //    .HasForeignKey(d => d.OrderId)
            //    //    .HasConstraintName("FK__order_ite__order__5BE2A6F2");

            //    //entity.HasOne(d => d.Product)
            //    //    .WithMany(p => p.OrderItems)
            //    //    .HasForeignKey(d => d.ProductId)
            //    //    .HasConstraintName("FK__order_ite__produ__5CD6CB2B");
            //});

            //modelBuilder.Entity<Order>(entity =>
            //{
            //    entity.HasKey(e => e.OrderId)
            //        .HasName("PK__orders__4659622945ED217A");

            //    entity.ToTable("Orders", "sales");

            //    entity.Property(e => e.OrderDate).HasColumnType("date");

            //    entity.HasOne(d => d.Store)
            //        .WithMany(p => p.Orders)
            //        .HasForeignKey(d => d.StoreId)
            //        .HasConstraintName("FK__orders__store_id__693CA210");

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.Orders)
            //        .HasForeignKey(d => d.UserId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK__orders__staff_id__59063A47");
            //});

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products", "production");


                entity.Property(e => e.ProductType)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.AccessoryItemType)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Cost).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FullRetailPrice).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Manufacturer)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MinimumSetPrice).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UPC)
                    .IsRequired()
                    .HasColumnName("UPC")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SerialNumber>(entity =>
            {
                entity.HasKey(e => e.SerialNumberValue)
                    .HasName("PK__SerialNu__EE62ACE4401AC986");

                entity.ToTable("SerialNumbers", "production");

                entity.Property(e => e.SerialNumberValue)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.SerialNumbers)
                    .HasForeignKey(d => new { d.StoreId, d.ProductId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SerialNumbers__07C12930");
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasKey(e => new { e.StoreId, e.ProductId })
                    .HasName("PK__stocks__E68284D3F3B1C262");

                entity.ToTable("Stocks", "production");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__Stocks__ProductI__0C85DE4D");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__Stocks__StoreId__0D7A0286");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(e => e.StoreId)
                    .HasName("PK__Stores__A2F2A30C6138CA99");

                entity.ToTable("Stores", "sales");

                entity.Property(e => e.City)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StoreName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StreetAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users", "sales");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__users__AB6E616452395AEB")
                    .IsUnique();

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(230)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Token)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.StreetAddress)
                    .IsRequired()
                    .HasMaxLength(230)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(230)
                    .IsUnicode(false);

                entity.Property(e => e.UserType)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .IsRequired()
                    .HasMaxLength(230)
                    .IsUnicode(false);

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__Users__StoreId__0A9D95DB");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<OrderItems>(entity =>
//            {
//                entity.HasKey(e => new { e.OrderId, e.ItemId })
//                    .HasName("PK__order_it__837942D4C00A9444");

//                entity.ToTable("order_items", "sales");

//                entity.Property(e => e.OrderId).HasColumnName("order_id");

//                entity.Property(e => e.ItemId).HasColumnName("item_id");

//                entity.Property(e => e.ListPrice)
//                    .HasColumnName("list_price")
//                    .HasColumnType("decimal(10, 2)");

//                entity.Property(e => e.ProductId).HasColumnName("product_id");

//                entity.Property(e => e.Quantity).HasColumnName("quantity");

//                entity.HasOne(d => d.Order)
//                    .WithMany(p => p.OrderItems)
//                    .HasForeignKey(d => d.OrderId)
//                    .HasConstraintName("FK__order_ite__order__5BE2A6F2");

//                entity.HasOne(d => d.Product)
//                    .WithMany(p => p.OrderItems)
//                    .HasForeignKey(d => d.ProductId)
//                    .HasConstraintName("FK__order_ite__produ__5CD6CB2B");
//            });

//            modelBuilder.Entity<Order>(entity =>
//            {
//                entity.HasKey(e => e.OrderId)
//                    .HasName("PK__orders__4659622945ED217A");

//                entity.ToTable("orders", "sales");

//                entity.Property(e => e.OrderId).HasColumnName("order_id");

//                entity.Property(e => e.OrderDate)
//                    .HasColumnName("order_date")
//                    .HasColumnType("date");

//                entity.Property(e => e.UserId).HasColumnName("staff_id");

//                entity.Property(e => e.StoreId).HasColumnName("store_id");

//                entity.HasOne(d => d.User)
//                    .WithMany(p => p.Orders)
//                    .HasForeignKey(d => d.UserId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK__orders__staff_id__59063A47");

//                entity.HasOne(d => d.Store)
//                    .WithMany(p => p.Orders)
//                    .HasForeignKey(d => d.StoreId)
//                    .HasConstraintName("FK__orders__store_id__5812160E");
//            });
//            modelBuilder.Entity<SerialNumber>(entity =>
//            {
//                entity.HasKey(e => e.SerialNumberValue)
//                    .HasName("PK__SerialNu__EE62ACE4401AC986");

//                entity.ToTable("SerialNumbers", "production");

//                entity.Property(e => e.SerialNumberValue)
//                    .HasMaxLength(20)
//                    .IsUnicode(false);

//                entity.HasOne(d => d.Stock)
//                    .WithMany(p => p.SerialNumbers)
//                    .HasForeignKey(d => new { d.StoreId, d.ProductId })
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK__SerialNumbers__07C12930");
//            });

//            modelBuilder.Entity<Product>(entity =>
//            {
//                //modelBuilder.Entity<Product>()
//            //entity.Property(p => p.Category).HasConversion(v => v.ToString(), v => (ProductType)Enum.Parse(typeof(ProductType), v)
//            //    );
//                //entity.Property(p => p.AccessoryItemType).HasConversion(v => v.ToString(), v => (AccessoryType)Enum.Parse(typeof(AccessoryType), v)
//                //);
//                entity.ToTable("Products", "production");

//                entity.Property(e => e.AccessoryItemType)
//                    .HasMaxLength(30)
//                    .IsUnicode(false);

//                entity.Property(e => e.Cost).HasColumnType("decimal(10, 2)");

//                entity.Property(e => e.Description)
//                    .HasMaxLength(255)
//                    .IsUnicode(false);

//                entity.Property(e => e.FullRetailPrice).HasColumnType("decimal(10, 2)");

//                entity.Property(e => e.IsActive).HasColumnName("IsActive");

//                entity.Property(e => e.Manufacturer)
//                    .HasMaxLength(20)
//                    .IsUnicode(false);

//                entity.Property(e => e.MinimumSetPrice).HasColumnType("decimal(10, 2)")
//                    .IsRequired()

//                    .IsUnicode(false);

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasMaxLength(255)
//                    .IsUnicode(false);

//                entity.Property(e => e.UPC)
//                    .IsRequired()
//                    .HasColumnName("UPC")
//                    .HasMaxLength(255)
//                    .IsUnicode(false);
//            });

//            modelBuilder.Entity<Stock>(entity =>
//            {

//                entity.HasKey(e => new { e.StoreId, e.ProductId })
//                  .HasName("PK__stocks__E68284D3F3B1C262");

//                entity.ToTable("Stocks", "production");

//                entity.HasOne(d => d.Product)
//                   .WithMany(p => p.Stocks)
//                   .HasForeignKey(d => d.ProductId)
//                   .HasConstraintName("FK__Stocks__ProductI__0C85DE4D");

//                entity.HasOne(d => d.Store)
//                    .WithMany(p => p.Stocks)
//                    .HasForeignKey(d => d.StoreId)
//                    .HasConstraintName("FK__Stocks__StoreId__0D7A0286");


//            });

//            modelBuilder.Entity<Store>(entity =>
//            {
//                entity.HasKey(e => e.StoreId)
//                   .HasName("PK__Stores__A2F2A30C6138CA99");

//                entity.ToTable("Stores", "sales");

//                entity.Property(e => e.City)
//                    .HasMaxLength(255)
//                    .IsUnicode(false);

//                entity.Property(e => e.PhoneNumber)
//                    .HasMaxLength(25)
//                    .IsUnicode(false);

//                entity.Property(e => e.State)
//                    .HasMaxLength(10)
//                    .IsUnicode(false);

//                entity.Property(e => e.StoreName)
//                    .IsRequired()
//                    .HasMaxLength(255)
//                    .IsUnicode(false);

//                entity.Property(e => e.StreetAddress)
//                    .HasMaxLength(255)
//                    .IsUnicode(false);

//                entity.Property(e => e.ZipCode)
//                    .HasMaxLength(5)
//                    .IsUnicode(false);
//            });

//            modelBuilder.Entity<User>(entity =>
//            {
//                entity.ToTable("Users", "sales");

//                entity.HasIndex(e => e.Email)
//                    .HasName("UQ__users__AB6E616452395AEB")
//                    .IsUnique();

//                entity.Property(e => e.City)
//                    .IsRequired()
//                    .HasMaxLength(230)
//                    .IsUnicode(false);

//                entity.Property(e => e.DateOfBirth).HasColumnType("date");

//                entity.Property(e => e.Email)
//                    .IsRequired()
//                    .HasMaxLength(255)
//                    .IsUnicode(false);

//                entity.Property(e => e.FirstName)
//                    .IsRequired()
//                    .HasMaxLength(50)
//                    .IsUnicode(false);

//                entity.Property(e => e.LastName)
//                    .IsRequired()
//                    .HasMaxLength(50)
//                    .IsUnicode(false);

//                entity.Property(e => e.Password)
//                    .IsRequired()
//                    .HasMaxLength(50)
//                    .IsUnicode(false);

//                entity.Property(e => e.PhoneNumber)
//                    .HasMaxLength(25)
//                    .IsUnicode(false);

//                entity.Property(e => e.State)
//                    .IsRequired()
//                    .HasMaxLength(5)
//                    .IsUnicode(false);

//                entity.Property(e => e.StreetAddress)
//                    .IsRequired()
//                    .HasMaxLength(230)
//                    .IsUnicode(false);

//                entity.Property(e => e.UserName)
//                    .HasMaxLength(230)
//                    .IsUnicode(false);

//                entity.Property(e => e.UserType)
//                    .IsRequired()
//                    .HasMaxLength(25)
//                    .IsUnicode(false);

//                entity.Property(e => e.ZipCode)
//                    .IsRequired()
//                    .HasMaxLength(230)
//                    .IsUnicode(false);

//                entity.HasOne(d => d.Store)
//                     .WithMany(p => p.Users)
//                     .HasForeignKey(d => d.StoreId)
//                     .HasConstraintName("FK__Users__StoreId__0A9D95DB");
//            });

//            OnModelCreatingPartial(modelBuilder);
//        }

//        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//    }

//}
