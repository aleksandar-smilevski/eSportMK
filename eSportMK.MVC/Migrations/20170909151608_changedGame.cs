using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eSportMK.MVC.Migrations
{
    public partial class changedGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_Games_GameId",
                table: "Tournaments");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Tournaments",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_Games_GameId",
                table: "Tournaments",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_Games_GameId",
                table: "Tournaments");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Tournaments",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_Games_GameId",
                table: "Tournaments",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
