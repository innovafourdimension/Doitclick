using Microsoft.EntityFrameworkCore.Migrations;

namespace Doitclick.Data.Migrations
{
    public partial class NuevosCamposVarios1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "ItemsCorizar",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TipoDuracionRetardo",
                table: "Etapas",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TipoDuracion",
                table: "Etapas",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagenOrdenSolicitante",
                table: "Cotizaciones",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "ItemsCorizar");

            migrationBuilder.DropColumn(
                name: "ImagenOrdenSolicitante",
                table: "Cotizaciones");

            migrationBuilder.AlterColumn<int>(
                name: "TipoDuracionRetardo",
                table: "Etapas",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "TipoDuracion",
                table: "Etapas",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
