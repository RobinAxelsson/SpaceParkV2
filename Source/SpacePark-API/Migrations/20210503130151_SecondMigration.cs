using Microsoft.EntityFrameworkCore.Migrations;

namespace SpacePark_API.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_SpacePort_SpacePortId",
                table: "Receipts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpacePort",
                table: "SpacePort");

            migrationBuilder.RenameTable(
                name: "SpacePort",
                newName: "SpacePorts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpacePorts",
                table: "SpacePorts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_SpacePorts_SpacePortId",
                table: "Receipts",
                column: "SpacePortId",
                principalTable: "SpacePorts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_SpacePorts_SpacePortId",
                table: "Receipts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpacePorts",
                table: "SpacePorts");

            migrationBuilder.RenameTable(
                name: "SpacePorts",
                newName: "SpacePort");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpacePort",
                table: "SpacePort",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_SpacePort_SpacePortId",
                table: "Receipts",
                column: "SpacePortId",
                principalTable: "SpacePort",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
