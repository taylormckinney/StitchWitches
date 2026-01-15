using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StitchWitches.Migrations
{
    /// <inheritdoc />
    public partial class changingSalesProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropColumn(
                name: "PaidByCard",
                table: "Sale");

            migrationBuilder.AddColumn<string>(
                name: "ItemsSold",
                table: "Sale",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethod",
                table: "Sale",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemsSold",
                table: "Sale");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Sale");

            migrationBuilder.AddColumn<bool>(
                name: "PaidByCard",
                table: "Sale",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InventoryItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SaleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Sale_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sale",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_SaleId",
                table: "OrderItem",
                column: "SaleId");
        }
    }
}
