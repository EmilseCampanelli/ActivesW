using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIAUTH.Data.Migrations
{
    /// <inheritdoc />
    public partial class _100initialdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Roles", new[] { "Description", "CreatedDate", "State" },
            new object[,]
            {
                { "Customer", DateTime.UtcNow, 1 },
                { "Admin", DateTime.UtcNow, 1 },
                { "Seller", DateTime.UtcNow, 1 }
            });

            // País
            migrationBuilder.InsertData("Countries", new[] { "Id", "Description", "CreatedDate", "State" },
                new object[] { 1, "Argentina", DateTime.UtcNow, 1 });

            // Provincias
            migrationBuilder.InsertData("Provinces", new[] { "Id", "Description", "CreatedDate", "State" },
                new object[,]
                {
            { 1, "Tucumán", DateTime.UtcNow, 1 },
            { 2, "Buenos Aires", DateTime.UtcNow, 1 }
                });

            // Categorías
            migrationBuilder.InsertData("Categories", new[] { "Id", "Description", "CreatedDate", "State", "ParentCategoryId" },
                new object[,]
                {
            { 1, "Pantalones", DateTime.UtcNow, 1, null },
            { 2, "Remeras", DateTime.UtcNow, 1, 1 },
            { 3, "Zapatillas", DateTime.UtcNow, 1, 1 }
                });

            // Compañía
            migrationBuilder.InsertData("Companies", new[] {
        "Id", "Name", "Email", "Phone", "Description", "CUIT", "Address", "OperationDate", "CreatedDate", "State"
    }, new object[] {
        1, "Active SW", "info@tech.com", "3811234567", "Empresa de Indumentaria", "20311222334", "San Miguel de Tucumán", DateTime.UtcNow, DateTime.UtcNow, 1
    });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
