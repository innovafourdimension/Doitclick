using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Doitclick.Data.Migrations
{
    public partial class NuevosCamposCotizacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DrSolicitante",
                table: "Cotizaciones",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EsOT",
                table: "Cotizaciones",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FolioSolicitante",
                table: "Cotizaciones",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Variables",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NumeroTicket = table.Column<string>(nullable: true),
                    Clave = table.Column<string>(nullable: true),
                    Valor = table.Column<string>(nullable: true),
                    Tipo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variables", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Variables");

            migrationBuilder.DropColumn(
                name: "DrSolicitante",
                table: "Cotizaciones");

            migrationBuilder.DropColumn(
                name: "EsOT",
                table: "Cotizaciones");

            migrationBuilder.DropColumn(
                name: "FolioSolicitante",
                table: "Cotizaciones");
        }
    }
}
