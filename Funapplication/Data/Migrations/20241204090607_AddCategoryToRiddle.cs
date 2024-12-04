using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Funapplication.Data.Migrations
{
    public partial class AddCategoryToRiddle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Riddle",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Riddle");
        }
    }
}
