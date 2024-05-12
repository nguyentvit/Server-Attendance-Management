using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class last : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "DayOffUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Waiting",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValueSql: "Waiting");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "DayOffUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValueSql: "Waiting",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Waiting");
        }
    }
}
