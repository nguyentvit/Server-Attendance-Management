using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedayoffv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DayOffUsers_DayOff_DayOffId",
                table: "DayOffUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DayOff",
                table: "DayOff");

            migrationBuilder.RenameTable(
                name: "DayOff",
                newName: "DayOffs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DayOffs",
                table: "DayOffs",
                column: "DayOffId");

            migrationBuilder.AddForeignKey(
                name: "FK_DayOffUsers_DayOffs_DayOffId",
                table: "DayOffUsers",
                column: "DayOffId",
                principalTable: "DayOffs",
                principalColumn: "DayOffId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DayOffUsers_DayOffs_DayOffId",
                table: "DayOffUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DayOffs",
                table: "DayOffs");

            migrationBuilder.RenameTable(
                name: "DayOffs",
                newName: "DayOff");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DayOff",
                table: "DayOff",
                column: "DayOffId");

            migrationBuilder.AddForeignKey(
                name: "FK_DayOffUsers_DayOff_DayOffId",
                table: "DayOffUsers",
                column: "DayOffId",
                principalTable: "DayOff",
                principalColumn: "DayOffId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
