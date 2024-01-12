using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pos_system.Migrations
{
    public partial class AdditionalReservations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Workers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDateTime",
                table: "Reservations",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ServiceId",
                table: "Reservations",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "Reservations",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkerId",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkerModelId",
                table: "Reservations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ServiceId",
                table: "Reservations",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_WorkerModelId",
                table: "Reservations",
                column: "WorkerModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Services_ServiceId",
                table: "Reservations",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Workers_WorkerModelId",
                table: "Reservations",
                column: "WorkerModelId",
                principalTable: "Workers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Services_ServiceId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Workers_WorkerModelId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ServiceId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_WorkerModelId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "WorkerModelId",
                table: "Reservations");

            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "Workers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StartDateTime",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ServiceId",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderModelId",
                table: "OrderServices",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderServices_Orders_OrderModelId",
                table: "OrderServices",
                column: "OrderModelId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
