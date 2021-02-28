using Microsoft.EntityFrameworkCore.Migrations;

namespace BashkiaRaporton.Data.Migrations
{
    public partial class Banore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nipt",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NjesiaAdministrativeId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NjesiaAdministrativeId",
                table: "AspNetUsers",
                column: "NjesiaAdministrativeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_njesiaAdministratives_NjesiaAdministrativeId",
                table: "AspNetUsers",
                column: "NjesiaAdministrativeId",
                principalTable: "njesiaAdministratives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_njesiaAdministratives_NjesiaAdministrativeId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_NjesiaAdministrativeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nipt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NjesiaAdministrativeId",
                table: "AspNetUsers");
        }
    }
}
