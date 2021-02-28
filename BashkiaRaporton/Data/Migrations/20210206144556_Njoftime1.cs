using Microsoft.EntityFrameworkCore.Migrations;

namespace BashkiaRaporton.Data.Migrations
{
    public partial class Njoftime1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Njoftime_AspNetUsers_BanoreId1",
                table: "Njoftime");

            migrationBuilder.DropIndex(
                name: "IX_Njoftime_BanoreId1",
                table: "Njoftime");

            migrationBuilder.DropColumn(
                name: "BanoreId1",
                table: "Njoftime");

            migrationBuilder.AlterColumn<string>(
                name: "BanoreId",
                table: "Njoftime",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Njoftime_BanoreId",
                table: "Njoftime",
                column: "BanoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Njoftime_AspNetUsers_BanoreId",
                table: "Njoftime",
                column: "BanoreId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Njoftime_AspNetUsers_BanoreId",
                table: "Njoftime");

            migrationBuilder.DropIndex(
                name: "IX_Njoftime_BanoreId",
                table: "Njoftime");

            migrationBuilder.AlterColumn<int>(
                name: "BanoreId",
                table: "Njoftime",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BanoreId1",
                table: "Njoftime",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Njoftime_BanoreId1",
                table: "Njoftime",
                column: "BanoreId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Njoftime_AspNetUsers_BanoreId1",
                table: "Njoftime",
                column: "BanoreId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
