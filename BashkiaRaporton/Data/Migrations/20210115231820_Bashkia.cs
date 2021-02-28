using Microsoft.EntityFrameworkCore.Migrations;

namespace BashkiaRaporton.Data.Migrations
{
    public partial class Bashkia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bashkia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmriKryetarit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Posta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Popullsia = table.Column<int>(type: "int", nullable: false),
                    Siperfaqja = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Pershkrimi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bashkia", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bashkia");
        }
    }
}
