using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using JPNFinalProject.Data.DatabaseModels;

namespace JPNFinalProject.Migrations
{
    [DbContext(typeof(JPNFinalProjectContext))]
    [Migration("20161128080749_DatabaseCahnges")]
    partial class DatabaseCahnges
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("JPNFinalProject.Data.DatabaseModels.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AddressID");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("AddressId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("JPNFinalProject.Data.DatabaseModels.Business", b =>
                {
                    b.Property<int>("BusinessId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("BusinessID");

                    b.Property<int>("AddressId")
                        .HasColumnName("AddressID");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(1);

                    b.Property<string>("OperationalHour")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("BusinessId");

                    b.HasIndex("AddressId");

                    b.ToTable("Business");
                });

            modelBuilder.Entity("JPNFinalProject.Data.DatabaseModels.BusinessOrder", b =>
                {
                    b.Property<int>("BusinessOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("BusinessOrderID");

                    b.Property<int>("BusinessId")
                        .HasColumnName("BusinessID");

                    b.Property<int>("OrderId")
                        .HasColumnName("OrderID");

                    b.HasKey("BusinessOrderId");

                    b.HasIndex("BusinessId");

                    b.HasIndex("OrderId");

                    b.ToTable("BusinessOrder");
                });

            modelBuilder.Entity("JPNFinalProject.Data.DatabaseModels.BusinessProduct", b =>
                {
                    b.Property<int>("BusinessProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("BusinessProductID");

                    b.Property<int>("BusinessId")
                        .HasColumnName("BusinessID");

                    b.Property<int>("ProductId")
                        .HasColumnName("ProductID");

                    b.HasKey("BusinessProductId");

                    b.HasIndex("BusinessId");

                    b.HasIndex("ProductId");

                    b.ToTable("BusinessProduct");
                });

            modelBuilder.Entity("JPNFinalProject.Data.DatabaseModels.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("OrderID");

                    b.Property<DateTimeOffset>("DeliveryTime");

                    b.Property<int>("PersonId")
                        .HasColumnName("PersonID");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal");

                    b.HasKey("OrderId");

                    b.HasIndex("PersonId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("JPNFinalProject.Data.DatabaseModels.OrderProduct", b =>
                {
                    b.Property<int>("OrderProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("OrderProductID");

                    b.Property<int>("Amount");

                    b.Property<int>("OrderId")
                        .HasColumnName("OrderID");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal");

                    b.Property<int>("ProductId")
                        .HasColumnName("ProductID");

                    b.HasKey("OrderProductId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProduct");
                });

            modelBuilder.Entity("JPNFinalProject.Data.DatabaseModels.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("PersonID");

                    b.Property<int>("AddressId")
                        .HasColumnName("AddressID");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("PersonId");

                    b.HasIndex("AddressId");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("JPNFinalProject.Data.DatabaseModels.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ProductID");

                    b.Property<int>("Amount");

                    b.Property<DateTimeOffset>("DeliveryTime");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("ImagePath");

                    b.Property<string>("ItemNumber")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal");

                    b.Property<int>("ProductCategoryID")
                        .HasColumnName("ProductCategoryID");

                    b.HasKey("ProductId");

                    b.HasIndex("ProductCategoryID");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("JPNFinalProject.Data.DatabaseModels.ProductCategory", b =>
                {
                    b.Property<int>("ProductCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ProductCategoryID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int?>("ParentID");

                    b.Property<string>("ProductText");

                    b.HasKey("ProductCategoryId");

                    b.HasIndex("ParentID");

                    b.ToTable("ProductCategory");
                });

            modelBuilder.Entity("JPNFinalProject.Data.DatabaseModels.Business", b =>
                {
                    b.HasOne("JPNFinalProject.Data.DatabaseModels.Address", "Address")
                        .WithMany("Business")
                        .HasForeignKey("AddressId")
                        .HasConstraintName("FK__Business__Addres__3493CFA7");
                });

            modelBuilder.Entity("JPNFinalProject.Data.DatabaseModels.BusinessOrder", b =>
                {
                    b.HasOne("JPNFinalProject.Data.DatabaseModels.Business", "Business")
                        .WithMany("BusinessOrder")
                        .HasForeignKey("BusinessId")
                        .HasConstraintName("FK__BusinessO__Busin__0C1BC9F9");

                    b.HasOne("JPNFinalProject.Data.DatabaseModels.Order", "Order")
                        .WithMany("BusinessOrder")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK__BusinessO__Order__0D0FEE32");
                });

            modelBuilder.Entity("JPNFinalProject.Data.DatabaseModels.BusinessProduct", b =>
                {
                    b.HasOne("JPNFinalProject.Data.DatabaseModels.Business", "Business")
                        .WithMany("BusinessProduct")
                        .HasForeignKey("BusinessId")
                        .HasConstraintName("FK__BusinessP__Busin__3F115E1A");

                    b.HasOne("JPNFinalProject.Data.DatabaseModels.Product", "Product")
                        .WithMany("BusinessProduct")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK__BusinessP__Produ__40058253");
                });

            modelBuilder.Entity("JPNFinalProject.Data.DatabaseModels.Order", b =>
                {
                    b.HasOne("JPNFinalProject.Data.DatabaseModels.Person", "Person")
                        .WithMany("Order")
                        .HasForeignKey("PersonId")
                        .HasConstraintName("FK__Order__PersonID__47A6A41B");
                });

            modelBuilder.Entity("JPNFinalProject.Data.DatabaseModels.OrderProduct", b =>
                {
                    b.HasOne("JPNFinalProject.Data.DatabaseModels.Order", "Order")
                        .WithMany("OrderProduct")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK__OrderProd__Order__00AA174D");

                    b.HasOne("JPNFinalProject.Data.DatabaseModels.Product", "Product")
                        .WithMany("OrderProduct")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK__OrderProd__Produ__019E3B86");
                });

            modelBuilder.Entity("JPNFinalProject.Data.DatabaseModels.Person", b =>
                {
                    b.HasOne("JPNFinalProject.Data.DatabaseModels.Address", "Address")
                        .WithMany("Person")
                        .HasForeignKey("AddressId")
                        .HasConstraintName("FK__Person__AddressI__42E1EEFE");
                });

            modelBuilder.Entity("JPNFinalProject.Data.DatabaseModels.Product", b =>
                {
                    b.HasOne("JPNFinalProject.Data.DatabaseModels.ProductCategory", "ProductCategory")
                        .WithMany("Product")
                        .HasForeignKey("ProductCategoryID")
                        .HasConstraintName("FK__Product__Product__3C34F16F");
                });

            modelBuilder.Entity("JPNFinalProject.Data.DatabaseModels.ProductCategory", b =>
                {
                    b.HasOne("JPNFinalProject.Data.DatabaseModels.ProductCategory", "ParentProductCategory")
                        .WithMany("Children")
                        .HasForeignKey("ParentID");
                });
        }
    }
}
