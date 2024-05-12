using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AttendanceManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateShiftv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_Departments_DepartmentId",
                table: "Shifts");

            migrationBuilder.DropTable(
                name: "RegisterShift");

            migrationBuilder.DropIndex(
                name: "IX_Shifts_DepartmentId",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Shifts");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Shifts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Shifts",
                columns: new[] { "ShiftId", "ApplicationUserId", "ShiftName", "Time_In", "Time_Out" },
                values: new object[,]
                {
                    { new Guid("13a27559-42be-4984-8e5c-55331f5a039f"), null, "Morning", new TimeSpan(0, 7, 30, 0, 0), new TimeSpan(0, 11, 30, 0, 0) },
                    { new Guid("3a6ffedc-25d6-45da-8354-b7880eced953"), null, "Afternoon", new TimeSpan(0, 14, 0, 0, 0), new TimeSpan(0, 17, 30, 0, 0) }
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_AspNetUsers_ApplicationUserId",
                table: "Shifts");

            migrationBuilder.DropIndex(
                name: "IX_Shifts_ApplicationUserId",
                table: "Shifts");

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("13a27559-42be-4984-8e5c-55331f5a039f"));

            migrationBuilder.DeleteData(
                table: "Shifts",
                keyColumn: "ShiftId",
                keyValue: new Guid("3a6ffedc-25d6-45da-8354-b7880eced953"));

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Shifts");

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "Shifts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "RegisterShift",
                columns: table => new
                {
                    ShiftsShiftId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterShift", x => new { x.ShiftsShiftId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RegisterShift_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegisterShift_Shifts_ShiftsShiftId",
                        column: x => x.ShiftsShiftId,
                        principalTable: "Shifts",
                        principalColumn: "ShiftId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_DepartmentId",
                table: "Shifts",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterShift_UsersId",
                table: "RegisterShift",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_Departments_DepartmentId",
                table: "Shifts",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
