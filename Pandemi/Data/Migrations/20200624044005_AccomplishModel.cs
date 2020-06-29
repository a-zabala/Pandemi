using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pandemi.Data.Migrations
{
    public partial class AccomplishModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accomplishments",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FamilyMemberID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accomplishments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Accomplishments_FamilyMembers_FamilyMemberID",
                        column: x => x.FamilyMemberID,
                        principalTable: "FamilyMembers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accomplishments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accomplishments_FamilyMemberID",
                table: "Accomplishments",
                column: "FamilyMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Accomplishments_UserId",
                table: "Accomplishments",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accomplishments");
        }
    }
}
