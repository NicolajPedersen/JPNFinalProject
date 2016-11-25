using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JPNFinalProject.Migrations
{
    public partial class Productcategoryforeign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategory_ProductCategory_CategoryProductCategoryId",
                table: "ProductCategory");

            migrationBuilder.DropColumn(
                name: "TestString",
                table: "ProductCategory");

            migrationBuilder.RenameColumn(
                name: "CategoryProductCategoryId",
                table: "ProductCategory",
                newName: "ProductCategoryId1");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCategory_CategoryProductCategoryId",
                table: "ProductCategory",
                newName: "IX_ProductCategory_ProductCategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategory_ProductCategory_ProductCategoryId1",
                table: "ProductCategory",
                column: "ProductCategoryId1",
                principalTable: "ProductCategory",
                principalColumn: "ProductCategoryID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategory_ProductCategory_ProductCategoryId1",
                table: "ProductCategory");

            migrationBuilder.RenameColumn(
                name: "ProductCategoryId1",
                table: "ProductCategory",
                newName: "CategoryProductCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCategory_ProductCategoryId1",
                table: "ProductCategory",
                newName: "IX_ProductCategory_CategoryProductCategoryId");

            migrationBuilder.AddColumn<string>(
                name: "TestString",
                table: "ProductCategory",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategory_ProductCategory_CategoryProductCategoryId",
                table: "ProductCategory",
                column: "CategoryProductCategoryId",
                principalTable: "ProductCategory",
                principalColumn: "ProductCategoryID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
