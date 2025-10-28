using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StitchWitches.Migrations
{
    /// <inheritdoc />
    public partial class ChangingImgToFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItem_UploadedImage_ImageId",
                table: "InventoryItem");

            migrationBuilder.DropTable(
                name: "UploadedImage");

            migrationBuilder.DropIndex(
                name: "IX_InventoryItem_ImageId",
                table: "InventoryItem");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "InventoryItem");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "InventoryItem",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "InventoryItem");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "InventoryItem",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UploadedImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadedImage", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItem_ImageId",
                table: "InventoryItem",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItem_UploadedImage_ImageId",
                table: "InventoryItem",
                column: "ImageId",
                principalTable: "UploadedImage",
                principalColumn: "Id");
        }
    }
}
