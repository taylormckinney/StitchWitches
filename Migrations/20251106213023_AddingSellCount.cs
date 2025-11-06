using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StitchWitches.Migrations
{
    /// <inheritdoc />
    public partial class AddingSellCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SellCount",
                table: "InventoryItem",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellCount",
                table: "InventoryItem");
        }
    }
}
