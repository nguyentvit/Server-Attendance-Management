using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class reset2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_AspNetUsers_ApplicationUserId",
                table: "Shifts");

            migrationBuilder.DropIndex(
                name: "IX_Shifts_ApplicationUserId",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Shifts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Shifts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("13a27559-42be-4984-8e5c-55331f5a039f"),
                column: "ApplicationUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("3a6ffedc-25d6-45da-8354-b7880eced953"),
                column: "ApplicationUserId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_ApplicationUserId",
                table: "Shifts",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_AspNetUsers_ApplicationUserId",
                table: "Shifts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
