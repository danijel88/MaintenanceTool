using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaintenanceTool.Infrastructure.Migrations
{
    public partial class SparepartImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "NextService",
                table: "Products",
                type: "DATE",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 25, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "DATE",
                oldDefaultValue: new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastService",
                table: "Products",
                type: "DATE",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 25, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "DATE",
                oldDefaultValue: new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.CreateTable(
                name: "SparePartImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SparePartId = table.Column<int>(type: "int", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SparePartImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SparePartImage_SpareParts_SparePartId",
                        column: x => x.SparePartId,
                        principalTable: "SpareParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_SparePartId",
                table: "Inventories",
                column: "SparePartId");

            migrationBuilder.CreateIndex(
                name: "IX_SparePartImage_SparePartId",
                table: "SparePartImage",
                column: "SparePartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_SpareParts_SparePartId",
                table: "Inventories",
                column: "SparePartId",
                principalTable: "SpareParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_SpareParts_SparePartId",
                table: "Inventories");

            migrationBuilder.DropTable(
                name: "SparePartImage");

            migrationBuilder.DropIndex(
                name: "IX_Inventories_SparePartId",
                table: "Inventories");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NextService",
                table: "Products",
                type: "DATE",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "DATE",
                oldDefaultValue: new DateTime(2024, 3, 25, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastService",
                table: "Products",
                type: "DATE",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "DATE",
                oldDefaultValue: new DateTime(2024, 3, 25, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
