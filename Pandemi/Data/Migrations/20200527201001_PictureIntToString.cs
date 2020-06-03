using Microsoft.EntityFrameworkCore.Migrations;

namespace Pandemi.Data.Migrations
{
    public partial class PictureIntToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pictures",
                table: "JournalEntries");

            migrationBuilder.AddColumn<string>(
                name: "EntryPicture",
                table: "JournalEntries",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntryPicture",
                table: "JournalEntries");

            migrationBuilder.AddColumn<int>(
                name: "Pictures",
                table: "JournalEntries",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
