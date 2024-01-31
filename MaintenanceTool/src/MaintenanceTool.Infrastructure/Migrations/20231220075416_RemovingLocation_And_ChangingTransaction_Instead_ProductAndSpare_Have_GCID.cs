using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaintenanceTool.Infrastructure.Migrations
{
    public partial class RemovingLocation_And_ChangingTransaction_Instead_ProductAndSpare_Have_GCID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Transactions",
                newName: "GroupConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_PlantId",
                table: "Groups",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupConfig_ProductId",
                table: "GroupConfig",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupConfig_SparePartId",
                table: "GroupConfig",
                column: "SparePartId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupConfig_Products_ProductId",
                table: "GroupConfig",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupConfig_SpareParts_SparePartId",
                table: "GroupConfig",
                column: "SparePartId",
                principalTable: "SpareParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Plants_PlantId",
                table: "Groups",
                column: "PlantId",
                principalTable: "Plants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupConfig_Products_ProductId",
                table: "GroupConfig");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupConfig_SpareParts_SparePartId",
                table: "GroupConfig");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Plants_PlantId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_PlantId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_GroupConfig_ProductId",
                table: "GroupConfig");

            migrationBuilder.DropIndex(
                name: "IX_GroupConfig_SparePartId",
                table: "GroupConfig");

            migrationBuilder.RenameColumn(
                name: "GroupConfigId",
                table: "Transactions",
                newName: "ProductId");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });
        }
    }
}
