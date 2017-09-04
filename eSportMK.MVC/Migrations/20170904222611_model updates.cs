using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eSportMK.MVC.Migrations
{
    public partial class modelupdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Players");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Players",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Players",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_CountryId",
                table: "Players",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_GameId",
                table: "Players",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Countries_CountryId",
                table: "Players",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Games_GameId",
                table: "Players",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Countries_CountryId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Games_GameId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_CountryId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_GameId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Players");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Players",
                nullable: true);
        }
    }
}
