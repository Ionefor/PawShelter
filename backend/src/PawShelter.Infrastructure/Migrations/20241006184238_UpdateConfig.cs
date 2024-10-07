using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawShelter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "health_info",
                table: "Pets");

            migrationBuilder.AddColumn<string>(
                name: "HealthInfo",
                table: "Pets",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HealthInfo",
                table: "Pets");

            migrationBuilder.AddColumn<string>(
                name: "health_info",
                table: "Pets",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");
        }
    }
}
