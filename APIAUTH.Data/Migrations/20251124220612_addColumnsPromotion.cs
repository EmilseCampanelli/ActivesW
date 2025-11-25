using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIAUTH.Data.Migrations
{
    /// <inheritdoc />
    public partial class addColumnsPromotion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MinQuantityRequired",
                table: "Promotion",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "WholesalePrice",
                table: "Promotion",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WholesaleQuantity",
                table: "Promotion",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinQuantityRequired",
                table: "Promotion");

            migrationBuilder.DropColumn(
                name: "WholesalePrice",
                table: "Promotion");

            migrationBuilder.DropColumn(
                name: "WholesaleQuantity",
                table: "Promotion");
        }
    }
}
