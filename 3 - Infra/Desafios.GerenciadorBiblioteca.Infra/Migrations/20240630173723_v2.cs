using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafios.GerenciadorBiblioteca.Infra.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Prone",
                table: "Libraries",
                newName: "Phone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Libraries",
                newName: "Prone");
        }
    }
}
