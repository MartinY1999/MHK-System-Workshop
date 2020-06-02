using Microsoft.EntityFrameworkCore.Migrations;

namespace MHKBackend.Migrations
{
    public partial class RefactoredDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Month",
                table: "Supports",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Supports",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Month",
                table: "Sales",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Sales",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Month",
                table: "Projects",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Projects",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Month",
                table: "Supports");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Supports");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Projects");
        }
    }
}
