using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedayoffv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DayOff",
                columns: table => new
                {
                    DayOffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayOff", x => x.DayOffId);
                });

            migrationBuilder.CreateTable(
                name: "DayOffUsers",
                columns: table => new
                {
                    DayOffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Waiting")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayOffUsers", x => new { x.UserId, x.DayOffId });
                    table.ForeignKey(
                        name: "FK_DayOffUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DayOffUsers_DayOff_DayOffId",
                        column: x => x.DayOffId,
                        principalTable: "DayOff",
                        principalColumn: "DayOffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DayOffUsers_DayOffId",
                table: "DayOffUsers",
                column: "DayOffId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayOffUsers");

            migrationBuilder.DropTable(
                name: "DayOff");
        }
    }
}
