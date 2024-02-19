using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaintenanceTool.Infrastructure.Migrations
{
    public partial class AddScheduledMaintenance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EachMonths",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastService",
                table: "Products",
                type: "DATE",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.AddColumn<DateTime>(
                name: "NextService",
                table: "Products",
                type: "DATE",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EachMonths",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LastService",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "NextService",
                table: "Products");
        }
    }
}
