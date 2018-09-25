using Microsoft.EntityFrameworkCore.Migrations;

namespace Doitclick.Data.Migrations
{
    public partial class CambioCampoUnidadMedida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnidadMedida",
                table: "MaterialesMensuales");

            migrationBuilder.AddColumn<int>(
                name: "UnidadMedidaId",
                table: "MaterialesMensuales",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MaterialesMensuales_UnidadMedidaId",
                table: "MaterialesMensuales",
                column: "UnidadMedidaId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialesMensuales_TiposUnidadMedidas_UnidadMedidaId",
                table: "MaterialesMensuales",
                column: "UnidadMedidaId",
                principalTable: "TiposUnidadMedidas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialesMensuales_TiposUnidadMedidas_UnidadMedidaId",
                table: "MaterialesMensuales");

            migrationBuilder.DropIndex(
                name: "IX_MaterialesMensuales_UnidadMedidaId",
                table: "MaterialesMensuales");

            migrationBuilder.DropColumn(
                name: "UnidadMedidaId",
                table: "MaterialesMensuales");

            migrationBuilder.AddColumn<int>(
                name: "UnidadMedida",
                table: "MaterialesMensuales",
                nullable: false,
                defaultValue: 0);
        }
    }
}
