using Microsoft.EntityFrameworkCore.Migrations;

namespace Pandemi.Data.Migrations
{
    public partial class ChangedEntryPicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntryPicture",
                table: "JournalEntries");

            migrationBuilder.AddColumn<string>(
                name: "EntryFile",
                table: "JournalEntries",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntryFile",
                table: "JournalEntries");

            migrationBuilder.AddColumn<string>(
                name: "EntryPicture",
                table: "JournalEntries",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
