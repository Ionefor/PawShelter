using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawShelter.Species.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial_SW : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Species");

            migrationBuilder.CreateTable(
                name: "species",
                schema: "Species",
                columns: table => new
                {
                    species_id = table.Column<Guid>(type: "uuid", nullable: false),
                    species = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_species", x => x.species_id);
                });

            migrationBuilder.CreateTable(
                name: "breeds",
                schema: "Species",
                columns: table => new
                {
                    breed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    breed = table.Column<string>(type: "text", nullable: false),
                    species_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_breeds", x => x.breed_id);
                    table.ForeignKey(
                        name: "fk_breeds_species_species_id",
                        column: x => x.species_id,
                        principalSchema: "Species",
                        principalTable: "species",
                        principalColumn: "species_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_breeds_species_id",
                schema: "Species",
                table: "breeds",
                column: "species_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "breeds",
                schema: "Species");

            migrationBuilder.DropTable(
                name: "species",
                schema: "Species");
        }
    }
}
