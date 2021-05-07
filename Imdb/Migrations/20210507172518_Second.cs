using Microsoft.EntityFrameworkCore.Migrations;

namespace Imdb.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BornYear",
                table: "Producer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Producer",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompanyDescription",
                table: "Company",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Company",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BornYear",
                table: "Producer");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Producer");

            migrationBuilder.DropColumn(
                name: "CompanyDescription",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Company");
        }
    }
}
