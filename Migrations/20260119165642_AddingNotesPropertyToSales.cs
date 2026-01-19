using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StitchWitches.Migrations
{
    /// <inheritdoc />
    public partial class AddingNotesPropertyToSales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Sale",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Sale");
        }
    }
}
