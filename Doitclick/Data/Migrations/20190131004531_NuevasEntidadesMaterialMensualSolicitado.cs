using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Doitclick.Data.Migrations
{
    public partial class NuevasEntidadesMaterialMensualSolicitado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SolicitudesMaterialesMensuales",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FechaSolicitud = table.Column<DateTime>(nullable: false),
                    Comentarios = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    FechaFinalizacion = table.Column<DateTime>(nullable: false),
                    ComentariosFin = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesMaterialesMensuales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialesMensualesSolicitados",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    MaterialMensualId = table.Column<int>(nullable: true),
                    SolicitudMaterialMensualId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialesMensualesSolicitados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialesMensualesSolicitados_MaterialesMensuales_MaterialM~",
                        column: x => x.MaterialMensualId,
                        principalTable: "MaterialesMensuales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialesMensualesSolicitados_SolicitudesMaterialesMensuale~",
                        column: x => x.SolicitudMaterialMensualId,
                        principalTable: "SolicitudesMaterialesMensuales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialesMensualesSolicitados_MaterialMensualId",
                table: "MaterialesMensualesSolicitados",
                column: "MaterialMensualId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialesMensualesSolicitados_SolicitudMaterialMensualId",
                table: "MaterialesMensualesSolicitados",
                column: "SolicitudMaterialMensualId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialesMensualesSolicitados");

            migrationBuilder.DropTable(
                name: "SolicitudesMaterialesMensuales");
        }
    }
}
