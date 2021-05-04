
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpacePark_API.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PriceMultiplier",
                table: "SpacePorts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceMultiplier",
                table: "SpacePorts");
        }
    }
}
