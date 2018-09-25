using Microsoft.EntityFrameworkCore.Migrations;

namespace Doitclick.Data.Migrations
{
    public partial class AgregarCampoCantidadMaterialMEnsual : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "MaterialesMensuales",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "UnidadMedida",
                table: "MaterialesDiponibles",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "MaterialesMensuales");

            migrationBuilder.AlterColumn<int>(
                name: "UnidadMedida",
                table: "MaterialesDiponibles",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
