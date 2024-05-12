using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adddayofftable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DayOffs",
                columns: table => new
                {
                    DayOffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayOffs", x => x.DayOffId);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserDayOff",
                columns: table => new
                {
                    DayOffsDayOffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserDayOff", x => new { x.DayOffsDayOffId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserDayOff_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserDayOff_DayOffs_DayOffsDayOffId",
                        column: x => x.DayOffsDayOffId,
                        principalTable: "DayOffs",
                        principalColumn: "DayOffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserDayOff_UsersId",
                table: "ApplicationUserDayOff",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserDayOff");

            migrationBuilder.DropTable(
                name: "DayOffs");
        }
    }
}
