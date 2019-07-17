using Microsoft.EntityFrameworkCore.Migrations;

namespace Doitclick.Data.Migrations
{
    public partial class AgregarSolicitanteYResolutorEnSolicitud : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RutResolutor",
                table: "SolicitudesMaterialesMensuales",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RutSolicitante",
                table: "SolicitudesMaterialesMensuales",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RutResolutor",
                table: "SolicitudesMaterialesMensuales");

            migrationBuilder.DropColumn(
                name: "RutSolicitante",
                table: "SolicitudesMaterialesMensuales");
        }
    }
}
