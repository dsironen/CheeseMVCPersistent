using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CheeseMVC.Migrations
{
    public partial class AddMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CheeseMenus",
                table: "CheeseMenus");

            migrationBuilder.DropIndex(
                name: "IX_CheeseMenus_CheeseID",
                table: "CheeseMenus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheeseMenus",
                table: "CheeseMenus",
                columns: new[] { "CheeseID", "MenuID" });

            migrationBuilder.CreateIndex(
                name: "IX_CheeseMenus_MenuID",
                table: "CheeseMenus",
                column: "MenuID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CheeseMenus",
                table: "CheeseMenus");

            migrationBuilder.DropIndex(
                name: "IX_CheeseMenus_MenuID",
                table: "CheeseMenus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheeseMenus",
                table: "CheeseMenus",
                columns: new[] { "MenuID", "CheeseID" });

            migrationBuilder.CreateIndex(
                name: "IX_CheeseMenus_CheeseID",
                table: "CheeseMenus",
                column: "CheeseID");
        }
    }
}
