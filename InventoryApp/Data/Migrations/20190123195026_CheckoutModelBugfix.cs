using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryApp.Data.Migrations
{
    public partial class CheckoutModelBugfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItem_Checkout_CheckoutID",
                table: "InventoryItem");

            migrationBuilder.DropIndex(
                name: "IX_InventoryItem_CheckoutID",
                table: "InventoryItem");

            migrationBuilder.DropColumn(
                name: "CheckoutID",
                table: "InventoryItem");

            migrationBuilder.AddColumn<int>(
                name: "InventoryItemID",
                table: "Checkout",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Checkout_InventoryItemID",
                table: "Checkout",
                column: "InventoryItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkout_InventoryItem_InventoryItemID",
                table: "Checkout",
                column: "InventoryItemID",
                principalTable: "InventoryItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkout_InventoryItem_InventoryItemID",
                table: "Checkout");

            migrationBuilder.DropIndex(
                name: "IX_Checkout_InventoryItemID",
                table: "Checkout");

            migrationBuilder.DropColumn(
                name: "InventoryItemID",
                table: "Checkout");

            migrationBuilder.AddColumn<int>(
                name: "CheckoutID",
                table: "InventoryItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItem_CheckoutID",
                table: "InventoryItem",
                column: "CheckoutID");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItem_Checkout_CheckoutID",
                table: "InventoryItem",
                column: "CheckoutID",
                principalTable: "Checkout",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
