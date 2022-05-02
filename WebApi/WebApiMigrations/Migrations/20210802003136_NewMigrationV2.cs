using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiMigrations.Migrations
{
    public partial class NewMigrationV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasMedicalInspection",
                table: "Kittens",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastInspection",
                table: "Kittens",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasMedicalInspection",
                table: "Kittens");

            migrationBuilder.DropColumn(
                name: "LastInspection",
                table: "Kittens");
        }
    }
}
