using Microsoft.EntityFrameworkCore.Migrations;

namespace Landmarks.Data.Migrations
{
    public partial class RatingSystem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                table: "Landmarks",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "RatingCount",
                table: "Landmarks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "RatingSum",
                table: "Landmarks",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "UserIdsRatedPic",
                table: "Landmarks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Landmarks");

            migrationBuilder.DropColumn(
                name: "RatingCount",
                table: "Landmarks");

            migrationBuilder.DropColumn(
                name: "RatingSum",
                table: "Landmarks");

            migrationBuilder.DropColumn(
                name: "UserIdsRatedPic",
                table: "Landmarks");
        }
    }
}
