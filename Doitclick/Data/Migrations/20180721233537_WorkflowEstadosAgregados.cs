using Microsoft.EntityFrameworkCore.Migrations;

namespace Doitclick.Data.Migrations
{
    public partial class WorkflowEstadosAgregados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Tareas",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Solicitudes",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Solicitudes");
        }
    }
}
