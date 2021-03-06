﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Landmarks.Data.Migrations
{
    public partial class VisitedPlaces : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VisitedPlaces",
                columns: table => new
                {
                    LandmarkId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitedPlaces", x => new { x.LandmarkId, x.UserId });
                    table.ForeignKey(
                        name: "FK_VisitedPlaces_Landmarks_LandmarkId",
                        column: x => x.LandmarkId,
                        principalTable: "Landmarks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VisitedPlaces_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisitedPlaces_UserId",
                table: "VisitedPlaces",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisitedPlaces");
        }
    }
}
