using Microsoft.EntityFrameworkCore.Migrations;

namespace BashkiaRaporton.Data.Migrations
{
    public partial class Order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Renditja",
                table: "Taksas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TaksaVlera",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vlera = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BashkiaId = table.Column<int>(type: "int", nullable: false),
                    TaksaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaksaVlera", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaksaVlera_Bashkia_BashkiaId",
                        column: x => x.BashkiaId,
                        principalTable: "Bashkia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaksaVlera_Taksas_TaksaId",
                        column: x => x.TaksaId,
                        principalTable: "Taksas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaksaVlera_BashkiaId",
                table: "TaksaVlera",
                column: "BashkiaId");

            migrationBuilder.CreateIndex(
                name: "IX_TaksaVlera_TaksaId",
                table: "TaksaVlera",
                column: "TaksaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaksaVlera");

            migrationBuilder.DropColumn(
                name: "Renditja",
                table: "Taksas");
        }
    }
}
