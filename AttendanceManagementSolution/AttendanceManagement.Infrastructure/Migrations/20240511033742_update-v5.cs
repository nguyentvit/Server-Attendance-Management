using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatev5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDayOffs");

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
                    table.PrimaryKey("PK_DayOffUsers", x => new { x.DayOffId, x.UserId });
                    table.ForeignKey(
                        name: "FK_DayOffUsers_AspNetUsers_UsersId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DayOffUsers_DayOffs_DayOffId",
                        column: x => x.DayOffId,
                        principalTable: "DayOffs",
                        principalColumn: "DayOffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DayOffUsers_UsersId",
                table: "DayOffUsers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayOffUsers");

            migrationBuilder.CreateTable(
                name: "UserDayOffs",
                columns: table => new
                {
                    DayOffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDayOffs", x => new { x.DayOffId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserDayOffs_AspNetUsers_UsersId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDayOffs_DayOffs_DayOffId",
                        column: x => x.DayOffId,
                        principalTable: "DayOffs",
                        principalColumn: "DayOffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDayOffs_UsersId",
                table: "UserDayOffs",
                column: "UserId");
        }
    }
}
