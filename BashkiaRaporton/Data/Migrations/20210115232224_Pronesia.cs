using Microsoft.EntityFrameworkCore.Migrations;

namespace BashkiaRaporton.Data.Migrations
{
    public partial class Pronesia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prona",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ndertesa = table.Column<double>(type: "float", nullable: false),
                    TokaBujqesore = table.Column<double>(type: "float", nullable: false),
                    Titulli = table.Column<double>(type: "float", nullable: false),
                    BanoreId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prona", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prona_AspNetUsers_BanoreId",
                        column: x => x.BanoreId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prona_BanoreId",
                table: "Prona",
                column: "BanoreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prona");
        }
    }
}
