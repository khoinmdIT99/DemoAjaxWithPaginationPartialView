using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BashkiaRaporton.Data.Migrations
{
    public partial class Njoftime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Njoftime",
                columns: table => new
                {
                    NjoftimeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BanoreId = table.Column<int>(type: "int", nullable: false),
                    BanoreId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Mesazhi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataDergimit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Shikushmeria = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Njoftime", x => x.NjoftimeId);
                    table.ForeignKey(
                        name: "FK_Njoftime_AspNetUsers_BanoreId1",
                        column: x => x.BanoreId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Njoftime_BanoreId1",
                table: "Njoftime",
                column: "BanoreId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Njoftime");
        }
    }
}
