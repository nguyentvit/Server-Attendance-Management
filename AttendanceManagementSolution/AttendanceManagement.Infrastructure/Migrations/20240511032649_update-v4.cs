using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatev4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDayOff_AspNetUsers_UsersId",
                table: "UserDayOff");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDayOff_DayOffs_DayOffId",
                table: "UserDayOff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDayOff",
                table: "UserDayOff");

            migrationBuilder.RenameTable(
                name: "UserDayOff",
                newName: "UserDayOffs");

            migrationBuilder.RenameIndex(
                name: "IX_UserDayOff_UsersId",
                table: "UserDayOffs",
                newName: "IX_UserDayOffs_UsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDayOffs",
                table: "UserDayOffs",
                columns: new[] { "DayOffId", "UsersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserDayOffs_AspNetUsers_UsersId",
                table: "UserDayOffs",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDayOffs_DayOffs_DayOffId",
                table: "UserDayOffs",
                column: "DayOffId",
                principalTable: "DayOffs",
                principalColumn: "DayOffId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDayOffs_AspNetUsers_UsersId",
                table: "UserDayOffs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDayOffs_DayOffs_DayOffId",
                table: "UserDayOffs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDayOffs",
                table: "UserDayOffs");

            migrationBuilder.RenameTable(
                name: "UserDayOffs",
                newName: "UserDayOff");

            migrationBuilder.RenameIndex(
                name: "IX_UserDayOffs_UsersId",
                table: "UserDayOff",
                newName: "IX_UserDayOff_UsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDayOff",
                table: "UserDayOff",
                columns: new[] { "DayOffId", "UsersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserDayOff_AspNetUsers_UsersId",
                table: "UserDayOff",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDayOff_DayOffs_DayOffId",
                table: "UserDayOff",
                column: "DayOffId",
                principalTable: "DayOffs",
                principalColumn: "DayOffId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
