using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIAUTH.Data.Migrations
{
    /// <inheritdoc />
    public partial class newMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "Ordenes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "Ordenes",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
