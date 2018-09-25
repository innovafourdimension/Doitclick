using Microsoft.EntityFrameworkCore.Migrations;

namespace Doitclick.Data.Migrations
{
    public partial class TiposUnidadMedidaPropagado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnidadMedida",
                table: "MaterialesDiponibles");

            /*migrationBuilder.AddColumn<float>(
                name: "PorcentajeComision",
                table: "Usuarios",
                nullable: false,
                defaultValue: 0f);*/

            /*migrationBuilder.AddColumn<bool>(
                name: "Comisionista",
                table: "Roles",
                nullable: false,
                defaultValue: false);*/

            migrationBuilder.AddColumn<int>(
                name: "UnidadMedidaId",
                table: "MaterialesDiponibles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MaterialesDiponibles_UnidadMedidaId",
                table: "MaterialesDiponibles",
                column: "UnidadMedidaId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialesDiponibles_TiposUnidadMedidas_UnidadMedidaId",
                table: "MaterialesDiponibles",
                column: "UnidadMedidaId",
                principalTable: "TiposUnidadMedidas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialesDiponibles_TiposUnidadMedidas_UnidadMedidaId",
                table: "MaterialesDiponibles");

            migrationBuilder.DropIndex(
                name: "IX_MaterialesDiponibles_UnidadMedidaId",
                table: "MaterialesDiponibles");

            /*migrationBuilder.DropColumn(
                name: "PorcentajeComision",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Comisionista",
                table: "Roles");*/

            migrationBuilder.DropColumn(
                name: "UnidadMedidaId",
                table: "MaterialesDiponibles");

            migrationBuilder.AddColumn<string>(
                name: "UnidadMedida",
                table: "MaterialesDiponibles",
                nullable: false,
                defaultValue: "");
        }
    }
}
