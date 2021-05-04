using Microsoft.EntityFrameworkCore.Migrations;

namespace SpacePark_API.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReceiptID",
                table: "Receipts",
                newName: "ReceiptId");

            migrationBuilder.AddColumn<int>(
                name: "SpacePortId",
                table: "Receipts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SpacePort",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParkingSpots = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpacePort", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_SpacePortId",
                table: "Receipts",
                column: "SpacePortId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_SpacePort_SpacePortId",
                table: "Receipts",
                column: "SpacePortId",
                principalTable: "SpacePort",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_SpacePort_SpacePortId",
                table: "Receipts");

            migrationBuilder.DropTable(
                name: "SpacePort");

            migrationBuilder.DropIndex(
                name: "IX_Receipts_SpacePortId",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "SpacePortId",
                table: "Receipts");

            migrationBuilder.RenameColumn(
                name: "ReceiptId",
                table: "Receipts",
                newName: "ReceiptID");
        }
    }
}
