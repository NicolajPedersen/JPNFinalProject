using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JPNFinalProject.Migrations
{
    public partial class productcategory7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropColumn(
                name: "TestString",
                table: "ProductCategory");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategory_ProductCategory_Parent",
                table: "ProductCategory",
                column: "Parent",
                principalTable: "ProductCategory",
                principalColumn: "ProductCategoryID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategory_ProductCategory_Parent",
                table: "ProductCategory");

            migrationBuilder.AddColumn<string>(
                name: "TestString",
                table: "ProductCategory",
                nullable: true);

        }
    }
}
