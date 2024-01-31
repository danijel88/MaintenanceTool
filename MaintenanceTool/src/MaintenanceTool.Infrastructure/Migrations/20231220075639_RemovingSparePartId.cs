using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaintenanceTool.Infrastructure.Migrations
{
    public partial class RemovingSparePartId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SparePartId",
                table: "Transactions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SparePartId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
