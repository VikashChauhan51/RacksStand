using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RacksStand.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SupplierCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Categories_Name_ShopId",
                table: "Categories",
                newName: "IX_Category_Name_ShopId");

            migrationBuilder.AddColumn<string>(
                name: "ShopId",
                table: "Suppliers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_Name_ShopId",
                table: "Suppliers",
                columns: new[] { "Name", "ShopId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_ShopId",
                table: "Suppliers",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Shops_ShopId",
                table: "Suppliers",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Shops_ShopId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_Name_ShopId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_ShopId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "Suppliers");

            migrationBuilder.RenameIndex(
                name: "IX_Category_Name_ShopId",
                table: "Categories",
                newName: "IX_Categories_Name_ShopId");
        }
    }
}
