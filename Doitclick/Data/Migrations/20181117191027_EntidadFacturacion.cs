using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Doitclick.Data.Migrations
{
    public partial class EntidadFacturacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EntidadFacturacionId",
                table: "Clientes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EntidadesFacturacion",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Rut = table.Column<string>(nullable: true),
                    RazonSocial = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    Giro = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntidadesFacturacion", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_EntidadFacturacionId",
                table: "Clientes",
                column: "EntidadFacturacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_EntidadesFacturacion_EntidadFacturacionId",
                table: "Clientes",
                column: "EntidadFacturacionId",
                principalTable: "EntidadesFacturacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_EntidadesFacturacion_EntidadFacturacionId",
                table: "Clientes");

            migrationBuilder.DropTable(
                name: "EntidadesFacturacion");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_EntidadFacturacionId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "EntidadFacturacionId",
                table: "Clientes");
        }
    }
}
