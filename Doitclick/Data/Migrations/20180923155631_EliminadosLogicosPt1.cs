using Microsoft.EntityFrameworkCore.Migrations;

namespace Doitclick.Data.Migrations
{
    public partial class EliminadosLogicosPt1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Activa",
                table: "TiposUnidadMedidas",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activa",
                table: "Servicios",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activa",
                table: "PrevisionesSalud",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activa",
                table: "MaterialesMensuales",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activa",
                table: "MaterialesDiponibles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activa",
                table: "Marcas",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activa",
                table: "Instrumentos",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activa",
                table: "TiposUnidadMedidas");

            migrationBuilder.DropColumn(
                name: "Activa",
                table: "Servicios");

            migrationBuilder.DropColumn(
                name: "Activa",
                table: "PrevisionesSalud");

            migrationBuilder.DropColumn(
                name: "Activa",
                table: "MaterialesMensuales");

            migrationBuilder.DropColumn(
                name: "Activa",
                table: "MaterialesDiponibles");

            migrationBuilder.DropColumn(
                name: "Activa",
                table: "Marcas");

            migrationBuilder.DropColumn(
                name: "Activa",
                table: "Instrumentos");
        }
    }
}
