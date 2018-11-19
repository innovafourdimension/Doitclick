using Microsoft.EntityFrameworkCore.Migrations;

namespace Doitclick.Data.Migrations
{
    public partial class PrevisionNoObligatoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_PrevisionesSalud_PrevisionSaludId",
                table: "Clientes");

            migrationBuilder.AlterColumn<int>(
                name: "PrevisionSaludId",
                table: "Clientes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_PrevisionesSalud_PrevisionSaludId",
                table: "Clientes",
                column: "PrevisionSaludId",
                principalTable: "PrevisionesSalud",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_PrevisionesSalud_PrevisionSaludId",
                table: "Clientes");

            migrationBuilder.AlterColumn<int>(
                name: "PrevisionSaludId",
                table: "Clientes",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_PrevisionesSalud_PrevisionSaludId",
                table: "Clientes",
                column: "PrevisionSaludId",
                principalTable: "PrevisionesSalud",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
