using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIAUTH.Data.Migrations
{
    /// <inheritdoc />
    public partial class _100InsertData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Categorías
            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Descripcion", "CreatedDate", "State" },
                values: new object[,]
                {
            { 1, "Remeras", DateTime.UtcNow, 1 },
            { 2, "Jeans", DateTime.UtcNow, 1 },
            { 3, "Zapatillas", DateTime.UtcNow, 1 },
            { 4, "Accesorios", DateTime.UtcNow, 1 }
                });

            // Países
            migrationBuilder.InsertData(
                table: "Pais",
                columns: new[] { "Id", "Descripcion", "CreatedDate", "State" },
                values: new object[,]
                {
            { 1, "Argentina", DateTime.UtcNow, 1 },
            { 2, "Uruguay", DateTime.UtcNow, 1 },
            { 3, "Chile", DateTime.UtcNow, 1 }
                });

            // Provincias
            migrationBuilder.InsertData(
                table: "Provincias",
                columns: new[] { "Id", "Descripcion", "CreatedDate", "State" },
                values: new object[,]
                {
            { 1, "Buenos Aires", DateTime.UtcNow, 1 },
            { 2, "Tucumán", DateTime.UtcNow, 1 },
            { 3, "Córdoba", DateTime.UtcNow, 1 }
                });

            // Roles
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Descripcion", "CreatedDate", "State" },
                values: new object[,]
                {
            { 1, "Admin", DateTime.UtcNow, 1 },
            { 2, "Vendedor", DateTime.UtcNow, 1 },
            { 3, "Cliente", DateTime.UtcNow, 1 }
                });

            // Tipos de Usuario
            migrationBuilder.InsertData(
                table: "UsuariosTipo",
                columns: new[] { "Id", "Descripcion", "CreatedDate", "State" },
                values: new object[,]
                {
            { 1, "Persona Física", DateTime.UtcNow, 1 },
            { 2, "Empresa", DateTime.UtcNow, 1 }
                });

            // Empresa
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Name", "Descripcion", "CUIT", "Direccion", "Email", "Telefono", "OperationDate", "CreatedDate", "State" },
                values: new object[]
                {
            1, "Blueberry Clothing", "Marca de ropa urbana", "30-12345678-9", "Av. Rivadavia 1234", "info@blueberry.com", "+54 381 1234567", DateTime.UtcNow, DateTime.UtcNow, 1
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
