using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawShelter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertyVO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_pets_volunteers_volunteer_id",
                table: "Pets");

            migrationBuilder.AddForeignKey(
                name: "fk_pets_volunteers_volunteer_id",
                table: "Pets",
                column: "volunteer_id",
                principalTable: "Volunteers",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_pets_volunteers_volunteer_id",
                table: "Pets");

            migrationBuilder.AddForeignKey(
                name: "fk_pets_volunteers_volunteer_id",
                table: "Pets",
                column: "volunteer_id",
                principalTable: "Volunteers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
