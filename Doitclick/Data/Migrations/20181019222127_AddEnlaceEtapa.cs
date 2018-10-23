using Microsoft.EntityFrameworkCore.Migrations;

namespace Doitclick.Data.Migrations
{
    public partial class AddEnlaceEtapa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Enlace",
                table: "Etapas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enlace",
                table: "Etapas");
        }
    }
}
