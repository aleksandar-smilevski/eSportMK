using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eSportMK.MVC.Migrations
{
    public partial class tourneyLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Countries_CountryId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Games_GameId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_Locations_LocationId",
                table: "Tournaments");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Tournaments",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Teams",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Teams",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nickname",
                table: "Players",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Countries_CountryId",
                table: "Teams",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Games_GameId",
                table: "Teams",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_Locations_LocationId",
                table: "Tournaments",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Countries_CountryId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Games_GameId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_Locations_LocationId",
                table: "Tournaments");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Tournaments",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Teams",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Teams",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Nickname",
                table: "Players",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Countries_CountryId",
                table: "Teams",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Games_GameId",
                table: "Teams",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_Locations_LocationId",
                table: "Tournaments",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
