using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Doitclick.Data.Migrations
{
    public partial class CotizacionesYFacturasExternas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CotizacionExternoId",
                table: "ItemsCorizar",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CotizacionesExternos",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 100, nullable: false),
                    RutPaciente = table.Column<string>(maxLength: 30, nullable: false),
                    NombrePaciente = table.Column<string>(nullable: true),
                    NumeroTicket = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    EntidadSolicitante = table.Column<string>(nullable: true),
                    OrdenFolio = table.Column<string>(nullable: true),
                    OrdenImagen = table.Column<string>(nullable: true),
                    Resumen = table.Column<string>(maxLength: 500, nullable: true),
                    PrecioTotal = table.Column<float>(nullable: false),
                    RequiereRetiro = table.Column<bool>(nullable: false),
                    DireccionRetiro = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CotizacionesExternos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 100, nullable: false),
                    FechaFacturacion = table.Column<DateTime>(nullable: false),
                    ValorFactura = table.Column<float>(nullable: false),
                    CotizacionFacturaId = table.Column<string>(nullable: true),
                    PagadorFacturaId = table.Column<Guid>(nullable: true),
                    Cerrada = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facturas_CotizacionesExternos_CotizacionFacturaId",
                        column: x => x.CotizacionFacturaId,
                        principalTable: "CotizacionesExternos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Facturas_EntidadesFacturacion_PagadorFacturaId",
                        column: x => x.PagadorFacturaId,
                        principalTable: "EntidadesFacturacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemsCorizar_CotizacionExternoId",
                table: "ItemsCorizar",
                column: "CotizacionExternoId");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_CotizacionFacturaId",
                table: "Facturas",
                column: "CotizacionFacturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_PagadorFacturaId",
                table: "Facturas",
                column: "PagadorFacturaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsCorizar_CotizacionesExternos_CotizacionExternoId",
                table: "ItemsCorizar",
                column: "CotizacionExternoId",
                principalTable: "CotizacionesExternos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemsCorizar_CotizacionesExternos_CotizacionExternoId",
                table: "ItemsCorizar");

            migrationBuilder.DropTable(
                name: "Facturas");

            migrationBuilder.DropTable(
                name: "CotizacionesExternos");

            migrationBuilder.DropIndex(
                name: "IX_ItemsCorizar_CotizacionExternoId",
                table: "ItemsCorizar");

            migrationBuilder.DropColumn(
                name: "CotizacionExternoId",
                table: "ItemsCorizar");
        }
    }
}
