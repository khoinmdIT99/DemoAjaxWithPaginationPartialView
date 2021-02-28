using Microsoft.EntityFrameworkCore.Migrations;

namespace BashkiaRaporton.Data.Migrations
{
    public partial class Njesia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "njesiaAdministratives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BashkiaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_njesiaAdministratives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_njesiaAdministratives_Bashkia_BashkiaId",
                        column: x => x.BashkiaId,
                        principalTable: "Bashkia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_njesiaAdministratives_BashkiaId",
                table: "njesiaAdministratives",
                column: "BashkiaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "njesiaAdministratives");
        }
    }
}
