using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateDepartmentTablever2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Departments_DepartmentId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "AspNetUsers",
                newName: "DeparmentId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_DepartmentId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_DeparmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Departments_DeparmentId",
                table: "AspNetUsers",
                column: "DeparmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Departments_DeparmentId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "DeparmentId",
                table: "AspNetUsers",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_DeparmentId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Departments_DepartmentId",
                table: "AspNetUsers",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId");
        }
    }
}
