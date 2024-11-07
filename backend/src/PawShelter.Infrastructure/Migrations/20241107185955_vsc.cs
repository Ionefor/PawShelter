using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawShelter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class vsc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "width",
                table: "pets",
                newName: "weight");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "weight",
                table: "pets",
                newName: "width");
        }
    }
}
