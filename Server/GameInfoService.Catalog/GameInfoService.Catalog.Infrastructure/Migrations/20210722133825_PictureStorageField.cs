using Microsoft.EntityFrameworkCore.Migrations;

namespace GameInfoService.Catalog.Infrastructure.Migrations
{
    public partial class PictureStorageField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PicturePath",
                table: "GameInfoSet",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PicturePath",
                table: "GameInfoSet");
        }
    }
}
