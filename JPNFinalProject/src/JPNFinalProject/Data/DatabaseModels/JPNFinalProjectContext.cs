using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JPNFinalProject.Data.DatabaseModels
{
    public partial class JPNFinalProjectContext : DbContext
    {
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Business> Business { get; set; }
        public virtual DbSet<BusinessOrder> BusinessOrder { get; set; }
        public virtual DbSet<BusinessProduct> BusinessProduct { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderProduct> OrderProduct { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            //optionsBuilder.UseSqlServer(@"Data Source=dev-db02\sql2016;Initial Catalog=JPNFinalProject;Persist Security Info=True;User ID=JPNFinalProject;Password=JPNFinalProject2016;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Business>(entity =>
            {
                entity.Property(e => e.BusinessId).HasColumnName("BusinessID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.OperationalHour)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Business)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__Business__Addres__3493CFA7");
            });

            modelBuilder.Entity<BusinessOrder>(entity =>
            {
                entity.Property(e => e.BusinessOrderId).HasColumnName("BusinessOrderID");

                entity.Property(e => e.BusinessId).HasColumnName("BusinessID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.BusinessOrder)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__BusinessO__Busin__0C1BC9F9");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.BusinessOrder)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__BusinessO__Order__0D0FEE32");
            });

            modelBuilder.Entity<BusinessProduct>(entity =>
            {
                entity.Property(e => e.BusinessProductId).HasColumnName("BusinessProductID");

                entity.Property(e => e.BusinessId).HasColumnName("BusinessID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.BusinessProduct)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__BusinessP__Busin__3F115E1A");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.BusinessProduct)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__BusinessP__Produ__40058253");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.TotalPrice).HasColumnType("decimal");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__Order__PersonID__47A6A41B");
            });

            modelBuilder.Entity<OrderProduct>(entity =>
            {
                entity.Property(e => e.OrderProductId).HasColumnName("OrderProductID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.Price).HasColumnType("decimal");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderProduct)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__OrderProd__Order__00AA174D");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderProduct)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__OrderProd__Produ__019E3B86");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__Person__AddressI__42E1EEFE");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.ItemNumber)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnType("decimal");

                entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProductCategoryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__Product__Product__3C34F16F");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });
        }
    }
}