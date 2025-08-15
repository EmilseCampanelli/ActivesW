using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIAUTH.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                INSERT INTO "Roles" ("Id","Description","CreatedDate","Status")
                VALUES 
                    (1,'Cliente', NOW(), 1),
                    (2,'Admin',    NOW(), 1),
                    (3,'Vendedor',   NOW(), 1)
                ON CONFLICT ("Id") DO NOTHING;
            """);

            // CATEGORIES (padre primero)
            migrationBuilder.Sql("""
                INSERT INTO "Categories" ("Id","Description","CreatedDate","Status","ParentCategoryId")
                VALUES 
                    (1,'Pantalones', NOW(), 1, NULL)
                ON CONFLICT ("Id") DO NOTHING;
            """);

            migrationBuilder.Sql("""
                INSERT INTO "Categories" ("Id","Description","CreatedDate","Status","ParentCategoryId")
                VALUES 
                    (2,'Remeras',    NOW(), 1, 1),
                    (3,'Zapatillas', NOW(), 1, 1)
                ON CONFLICT ("Id") DO NOTHING;
            """);

            // COMPANIES
            // ⚠️ Ajustá las columnas a tu entidad real. Si tu modelo tiene AddressId (FK), no intentes insertar "Address" texto.
            // Aquí un ejemplo mínimo sin dirección:
            migrationBuilder.Sql("""
                INSERT INTO "Companies" ("Id","Name","Email","Phone","Description","CUIT","OperationDate","CreatedDate","Status")
                VALUES (1,'Active SW','info@tech.com','3811234567','Empresa de Indumentaria','20311222334', NOW(), NOW(), 1)
                ON CONFLICT ("Id") DO NOTHING;
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""DELETE FROM "Companies"  WHERE "Id" IN (1);""");
            migrationBuilder.Sql("""DELETE FROM "Categories" WHERE "Id" IN (2,3);""");
            migrationBuilder.Sql("""DELETE FROM "Categories" WHERE "Id" IN (1);""");
            migrationBuilder.Sql("""DELETE FROM "Roles"      WHERE "Id" IN (1,2,3);""");
        }
    }
}
