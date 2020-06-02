using Microsoft.EntityFrameworkCore.Migrations;

namespace MHKBackend.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Finances",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BSPE = table.Column<decimal>(nullable: false),
                    Minimum = table.Column<int>(nullable: false),
                    Maximum = table.Column<int>(nullable: false),
                    Month = table.Column<string>(maxLength: 50, nullable: false),
                    Year = table.Column<int>(nullable: false),
                    MonthlySum = table.Column<decimal>(nullable: false),
                    PeopleCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeightNK = table.Column<decimal>(nullable: false),
                    Minimum = table.Column<int>(nullable: false),
                    Maximum = table.Column<int>(nullable: false),
                    TotalTasks = table.Column<int>(nullable: false),
                    TasksOnTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeightNK = table.Column<decimal>(nullable: false),
                    Minimum = table.Column<int>(nullable: false),
                    Maximum = table.Column<int>(nullable: false),
                    TotalTasks = table.Column<int>(nullable: false),
                    TasksOnTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Supports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeightNK = table.Column<decimal>(nullable: false),
                    Minimum = table.Column<int>(nullable: false),
                    Maximum = table.Column<int>(nullable: false),
                    TotalTasks = table.Column<int>(nullable: false),
                    TasksOnTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supports", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Finances");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Supports");
        }
    }
}
