using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Doitclick.Data.Migrations
{
    public partial class AgregarEntidadMovimientoMaterialMensual : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovimientosMaterialesMensuales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Folio = table.Column<string>(nullable: true),
                    RutSolicitante = table.Column<string>(nullable: true),
                    FechaSolicitud = table.Column<DateTime>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    FechaEstado = table.Column<DateTime>(nullable: false),
                    MaterialId = table.Column<int>(nullable: true),
                    Cantidad = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientosMaterialesMensuales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimientosMaterialesMensuales_MaterialesMensuales_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "MaterialesMensuales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosMaterialesMensuales_MaterialId",
                table: "MovimientosMaterialesMensuales",
                column: "MaterialId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimientosMaterialesMensuales");
        }
    }
}
