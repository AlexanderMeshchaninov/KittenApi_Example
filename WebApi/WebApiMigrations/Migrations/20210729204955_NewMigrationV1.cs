using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiMigrations.Migrations
{
    public partial class NewMigrationV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KittenId",
                table: "Clinics",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KittenId",
                table: "Clinics");
        }
    }
}
