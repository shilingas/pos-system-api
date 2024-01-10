using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pos_system.Migrations
{
    public partial class foreignKeyUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Products_ProductModelId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ProductModelId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductModelId",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "OrderProducts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            /*migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ProductId",
                table: "OrderProducts",
                column: "ProductId");*/

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Products_ProductId1",
                table: "OrderProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_ProductId",
                table: "OrderProducts");

            migrationBuilder.AddColumn<string>(
                name: "ProductModelId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "OrderProducts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductModelId",
                table: "Orders",
                column: "ProductModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Products_ProductModelId",
                table: "Orders",
                column: "ProductModelId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
