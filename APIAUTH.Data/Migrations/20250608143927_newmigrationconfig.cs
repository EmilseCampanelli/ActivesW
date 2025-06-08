using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIAUTH.Data.Migrations
{
    /// <inheritdoc />
    public partial class newmigrationconfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Accounts_AccountId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "Users",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "ColorThemes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Mode = table.Column<string>(type: "text", nullable: true),
                    Section = table.Column<string>(type: "text", nullable: true),
                    Property = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorThemes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "text", nullable: true),
                    Path = table.Column<string>(type: "text", nullable: true),
                    Icon = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "text", nullable: true),
                    Path = table.Column<string>(type: "text", nullable: true),
                    MenuId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItems_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ColorThemes",
                columns: new[] { "Id", "Mode", "Property", "Section", "Value" },
                values: new object[,]
                {
                    { 1, "light", "main", "primary", "#FFFFFF" },
                    { 2, "light", "contrastText", "primary", "#000000" },
                    { 3, "light", "main", "secondary", "#000000" },
                    { 4, "light", "contrastText", "secondary", "#FFFFFF" },
                    { 5, "light", "default", "background", "#FFFFFF" },
                    { 6, "light", "paper", "background", "#F9F9F9" },
                    { 7, "light", "primary", "text", "#000000" },
                    { 8, "light", "secondary", "text", "#666666" },
                    { 9, "light", "disabled", "text", "#BBBBBB" },
                    { 10, "light", "main", "border", "#DDDDDD" },
                    { 11, "light", "main", "success", "#4CAF50" },
                    { 12, "light", "contrastText", "success", "#FFFFFF" },
                    { 13, "light", "main", "warning", "#FFC107" },
                    { 14, "light", "contrastText", "warning", "#000000" },
                    { 15, "light", "main", "error", "#F44336" },
                    { 16, "light", "contrastText", "error", "#FFFFFF" },
                    { 17, "dark", "main", "primary", "#000000" },
                    { 18, "dark", "contrastText", "primary", "#FFFFFF" },
                    { 19, "dark", "main", "secondary", "#FFFFFF" },
                    { 20, "dark", "contrastText", "secondary", "#000000" },
                    { 21, "dark", "default", "background", "#121212" },
                    { 22, "dark", "paper", "background", "#1E1E1E" },
                    { 23, "dark", "primary", "text", "#FFFFFF" },
                    { 24, "dark", "secondary", "text", "#AAAAAA" },
                    { 25, "dark", "disabled", "text", "#555555" },
                    { 26, "dark", "main", "border", "#333333" },
                    { 27, "dark", "main", "success", "#81C784" },
                    { 28, "dark", "contrastText", "success", "#000000" },
                    { 29, "dark", "main", "warning", "#FFD54F" },
                    { 30, "dark", "contrastText", "warning", "#000000" },
                    { 31, "dark", "main", "error", "#E57373" },
                    { 32, "dark", "contrastText", "error", "#000000" }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Icon", "Label", "Path" },
                values: new object[,]
                {
                    { 1, "dashboard_icon", "Dashboard", "/dashboard" },
                    { 2, "products_icon", "Products", "/products" },
                    { 3, "users_icon", "Users", "/users" }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Label", "MenuId", "Path" },
                values: new object[,]
                {
                    { 1, "Product List", 2, "/products/list" },
                    { 2, "Add Product", 2, "/products/add" },
                    { 3, "User List", 3, "/users/list" },
                    { 4, "Add User", 3, "/users/add" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_MenuId",
                table: "MenuItems",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Accounts_AccountId",
                table: "Users",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Accounts_AccountId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "ColorThemes");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Accounts_AccountId",
                table: "Users",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
