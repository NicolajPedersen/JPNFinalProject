using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JPNFinalProject.Migrations
{
    public partial class DatabaseCahnges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategory_ProductCategory_Parent",
                table: "ProductCategory");

            migrationBuilder.DropColumn(
                name: "Business",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "Parent",
                table: "ProductCategory",
                newName: "ParentID");

            //migrationBuilder.RenameIndex(
            //    name: "IX_ProductCategory_Parent",
            //    table: "ProductCategory",
            //    newName: "IX_ProductCategory_ParentID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategory_ProductCategory_ParentID",
                table: "ProductCategory",
                column: "ParentID",
                principalTable: "ProductCategory",
                principalColumn: "ProductCategoryID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategory_ProductCategory_ParentID",
                table: "ProductCategory");

            migrationBuilder.RenameColumn(
                name: "ParentID",
                table: "ProductCategory",
                newName: "ParentProductCategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCategory_ParentID",
                table: "ProductCategory",
                newName: "IX_ProductCategory_ParentProductCategoryID");

            migrationBuilder.AddColumn<int>(
                name: "Business",
                table: "Product",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategory_ProductCategory_ParentProductCategoryID",
                table: "ProductCategory",
                column: "ParentProductCategoryID",
                principalTable: "ProductCategory",
                principalColumn: "ProductCategoryID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
