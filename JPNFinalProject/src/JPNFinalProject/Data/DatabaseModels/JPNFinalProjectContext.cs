using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JPNFinalProject.Data.DatabaseModels
{
    public partial class JPNFinalProjectContext : DbContext
    {
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Business> Business { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }

        // Unable to generate entity type for table 'dbo.BusinessProduct'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            //optionsBuilder.UseSqlServer(@"Data Source=dev-db02\sql2016;Initial Catalog=JPNFinalProject;User ID=JPNFinalProject;Password=JPNFinalProject2016;");
        }

        public JPNFinalProjectContext(DbContextOptions<JPNFinalProjectContext> options)
            : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
                    .HasMaxLength(255);

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
                    .HasConstraintName("FK__Business__Addres__08B54D69");
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
                    .HasConstraintName("FK__Order__PersonID__1AD3FDA4");
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
                    .HasConstraintName("FK__Person__AddressI__160F4887");
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
                    .HasConstraintName("FK__Product__Product__10566F31");
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