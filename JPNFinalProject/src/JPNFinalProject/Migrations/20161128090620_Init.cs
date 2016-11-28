using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JPNFinalProject.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    City = table.Column<string>(maxLength: 255, nullable: false),
                    Country = table.Column<string>(maxLength: 255, nullable: false),
                    PostalCode = table.Column<string>(maxLength: 20, nullable: false),
                    Street = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressID);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    ProductCategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    ParentID = table.Column<int>(nullable: true),
                    ProductText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.ProductCategoryID);
                    table.ForeignKey(
                        name: "FK_ProductCategory_ProductCategory_ParentID",
                        column: x => x.ParentID,
                        principalTable: "ProductCategory",
                        principalColumn: "ProductCategoryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Business",
                columns: table => new
                {
                    BusinessID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressID = table.Column<int>(nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 1, nullable: false),
                    OperationalHour = table.Column<string>(maxLength: 255, nullable: false),
                    Phone = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Business", x => x.BusinessID);
                    table.ForeignKey(
                        name: "FK__Business__Addres__3493CFA7",
                        column: x => x.AddressID,
                        principalTable: "Address",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    PersonID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressID = table.Column<int>(nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Password = table.Column<string>(maxLength: 255, nullable: false),
                    Phone = table.Column<string>(maxLength: 20, nullable: false),
                    Type = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonID);
                    table.ForeignKey(
                        name: "FK__Person__AddressI__42E1EEFE",
                        column: x => x.AddressID,
                        principalTable: "Address",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<int>(nullable: false),
                    DeliveryTime = table.Column<DateTimeOffset>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    ImagePath = table.Column<string>(nullable: true),
                    ItemNumber = table.Column<string>(maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Price = table.Column<decimal>(type: "decimal", nullable: false),
                    ProductCategoryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK__Product__Product__3C34F16F",
                        column: x => x.ProductCategoryID,
                        principalTable: "ProductCategory",
                        principalColumn: "ProductCategoryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeliveryTime = table.Column<DateTimeOffset>(nullable: false),
                    PersonID = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK__Order__PersonID__47A6A41B",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BusinessProduct",
                columns: table => new
                {
                    BusinessProductID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BusinessID = table.Column<int>(nullable: false),
                    ProductID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessProduct", x => x.BusinessProductID);
                    table.ForeignKey(
                        name: "FK__BusinessP__Busin__3F115E1A",
                        column: x => x.BusinessID,
                        principalTable: "Business",
                        principalColumn: "BusinessID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__BusinessP__Produ__40058253",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BusinessOrder",
                columns: table => new
                {
                    BusinessOrderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BusinessID = table.Column<int>(nullable: false),
                    OrderID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessOrder", x => x.BusinessOrderID);
                    table.ForeignKey(
                        name: "FK__BusinessO__Busin__0C1BC9F9",
                        column: x => x.BusinessID,
                        principalTable: "Business",
                        principalColumn: "BusinessID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__BusinessO__Order__0D0FEE32",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderProduct",
                columns: table => new
                {
                    OrderProductID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<int>(nullable: false),
                    OrderID = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal", nullable: false),
                    ProductID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProduct", x => x.OrderProductID);
                    table.ForeignKey(
                        name: "FK__OrderProd__Order__00AA174D",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__OrderProd__Produ__019E3B86",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Business_AddressID",
                table: "Business",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessOrder_BusinessID",
                table: "BusinessOrder",
                column: "BusinessID");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessOrder_OrderID",
                table: "BusinessOrder",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessProduct_BusinessID",
                table: "BusinessProduct",
                column: "BusinessID");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessProduct_ProductID",
                table: "BusinessProduct",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_PersonID",
                table: "Order",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_OrderID",
                table: "OrderProduct",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_ProductID",
                table: "OrderProduct",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Person_AddressID",
                table: "Person",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductCategoryID",
                table: "Product",
                column: "ProductCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_ParentID",
                table: "ProductCategory",
                column: "ParentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessOrder");

            migrationBuilder.DropTable(
                name: "BusinessProduct");

            migrationBuilder.DropTable(
                name: "OrderProduct");

            migrationBuilder.DropTable(
                name: "Business");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
