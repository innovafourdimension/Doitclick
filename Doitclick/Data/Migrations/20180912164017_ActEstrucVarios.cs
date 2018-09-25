using Microsoft.EntityFrameworkCore.Migrations;

namespace Doitclick.Data.Migrations
{
    public partial class ActEstrucVarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstadoEvaluacion",
                table: "Cotizaciones");

            migrationBuilder.AddColumn<float>(
                name: "PorcentajeComision",
                table: "Usuarios",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<bool>(
                name: "Comisionista",
                table: "Roles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "TipoCliente",
                table: "Clientes",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PorcentajeComision",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Comisionista",
                table: "Roles");

            migrationBuilder.AddColumn<string>(
                name: "EstadoEvaluacion",
                table: "Cotizaciones",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "TipoCliente",
                table: "Clientes",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
