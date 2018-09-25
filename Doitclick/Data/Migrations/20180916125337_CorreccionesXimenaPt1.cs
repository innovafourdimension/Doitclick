using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Doitclick.Data.Migrations
{
    public partial class CorreccionesXimenaPt1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "MaterialesMensuales",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MarcaId",
                table: "MaterialesMensuales",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "MaterialesDiponibles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MarcaId",
                table: "MaterialesDiponibles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Instrumentos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Instrumentos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MarcaId",
                table: "Instrumentos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialesMensuales_MarcaId",
                table: "MaterialesMensuales",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialesDiponibles_MarcaId",
                table: "MaterialesDiponibles",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Instrumentos_MarcaId",
                table: "Instrumentos",
                column: "MarcaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Instrumentos_Marcas_MarcaId",
                table: "Instrumentos",
                column: "MarcaId",
                principalTable: "Marcas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialesDiponibles_Marcas_MarcaId",
                table: "MaterialesDiponibles",
                column: "MarcaId",
                principalTable: "Marcas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialesMensuales_Marcas_MarcaId",
                table: "MaterialesMensuales",
                column: "MarcaId",
                principalTable: "Marcas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instrumentos_Marcas_MarcaId",
                table: "Instrumentos");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialesDiponibles_Marcas_MarcaId",
                table: "MaterialesDiponibles");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialesMensuales_Marcas_MarcaId",
                table: "MaterialesMensuales");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropIndex(
                name: "IX_MaterialesMensuales_MarcaId",
                table: "MaterialesMensuales");

            migrationBuilder.DropIndex(
                name: "IX_MaterialesDiponibles_MarcaId",
                table: "MaterialesDiponibles");

            migrationBuilder.DropIndex(
                name: "IX_Instrumentos_MarcaId",
                table: "Instrumentos");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "MaterialesMensuales");

            migrationBuilder.DropColumn(
                name: "MarcaId",
                table: "MaterialesMensuales");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "MaterialesDiponibles");

            migrationBuilder.DropColumn(
                name: "MarcaId",
                table: "MaterialesDiponibles");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Instrumentos");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Instrumentos");

            migrationBuilder.DropColumn(
                name: "MarcaId",
                table: "Instrumentos");
        }
    }
}
