using Microsoft.EntityFrameworkCore.Migrations;

namespace Doitclick.Data.Migrations
{
    public partial class AgregaNuevoCampoCotizacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.AlterColumn<string>(
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
                oldNullable: true);*/

            migrationBuilder.AddColumn<string>(
                name: "EstadoEvaluacion",
                table: "Cotizaciones",
                nullable: false
                //defaultValue: ""
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstadoEvaluacion",
                table: "Cotizaciones");

            /*migrationBuilder.AlterColumn<int>(
                name: "TipoDuracionRetardo",
                table: "Etapas",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "TipoDuracion",
                table: "Etapas",
                nullable: true,
                oldClrType: typeof(string));*/
        }
    }
}
