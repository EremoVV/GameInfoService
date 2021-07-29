using Microsoft.EntityFrameworkCore.Migrations;

namespace GameInfoService.Catalog.Infrastructure.Migrations
{
    public partial class DeveloperAdditionToEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeveloperId",
                table: "GameInfoSet",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameInfoSet_DeveloperId",
                table: "GameInfoSet",
                column: "DeveloperId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameInfoSet_GameDeveloperSet_DeveloperId",
                table: "GameInfoSet",
                column: "DeveloperId",
                principalTable: "GameDeveloperSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameInfoSet_GameDeveloperSet_DeveloperId",
                table: "GameInfoSet");

            migrationBuilder.DropIndex(
                name: "IX_GameInfoSet_DeveloperId",
                table: "GameInfoSet");

            migrationBuilder.DropColumn(
                name: "DeveloperId",
                table: "GameInfoSet");
        }
    }
}
