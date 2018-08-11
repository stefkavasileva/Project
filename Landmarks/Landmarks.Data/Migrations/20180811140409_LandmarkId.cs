using Microsoft.EntityFrameworkCore.Migrations;

namespace Landmarks.Data.Migrations
{
    public partial class LandmarkId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Landmarks_LandmarkId",
                table: "Image");

            migrationBuilder.AlterColumn<int>(
                name: "LandmarkId",
                table: "Image",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Landmarks_LandmarkId",
                table: "Image",
                column: "LandmarkId",
                principalTable: "Landmarks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Landmarks_LandmarkId",
                table: "Image");

            migrationBuilder.AlterColumn<int>(
                name: "LandmarkId",
                table: "Image",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Landmarks_LandmarkId",
                table: "Image",
                column: "LandmarkId",
                principalTable: "Landmarks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
