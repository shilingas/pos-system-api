using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pos_system.Migrations
{
    public partial class UpdatingOrderProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropColumn(
                name: "Discount",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ValidUntilDateTime",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "CouponModel",
                newName: "Coupons");*/

            migrationBuilder.AddColumn<string>(
                name: "ProductModelId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            /*migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "OrderProducts",
                type: "nvarchar(max)",
                nullable: true);*/

            /*migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Coupons",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "Coupons",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Coupons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ValidUntilDateTime",
                table: "Coupons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coupons",
                table: "Coupons",
                column: "Id");*/

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Products_ProductModelId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ProductModelId",
                table: "Orders");

            /*migrationBuilder.DropPrimaryKey(
                name: "PK_Coupons",
                table: "Coupons");*/

            migrationBuilder.DropColumn(
                name: "ProductModelId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "OrderProducts");

            /*migrationBuilder.DropColumn(
                name: "Id",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "ValidUntilDateTime",
                table: "Coupons");

            migrationBuilder.RenameTable(
                name: "Coupons",
                newName: "CouponModel");

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "Customers",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ValidUntilDateTime",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);*/
        }
    }
}
