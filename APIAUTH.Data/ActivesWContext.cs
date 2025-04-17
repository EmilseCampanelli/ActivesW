using Microsoft.EntityFrameworkCore;
using APIAUTH.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace APIAUTH.Data.Context
{
    public class ActivesWContext : DbContext
    {
        public ActivesWContext(DbContextOptions<ActivesWContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Empresa> Companies { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<UsuarioTipo> UsuariosTipo { get; set; }
        public DbSet<Notificacion> Notifications { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<Domicilio> Domicilios { get; set; }
        public DbSet<Carrito> Carritos { get; set; }
        public DbSet<CarritoProducto> CarritoProductos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Favorito> Favoritos { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<Producto> Productos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().Navigation(e => e.Rol).AutoInclude();
            modelBuilder.Entity<Usuario>().Navigation(e => e.Empresa).AutoInclude();
            modelBuilder.Entity<Usuario>().Navigation(e => e.Cuenta).AutoInclude();
            modelBuilder.Entity<Usuario>().Navigation(e => e.UsuarioTipo).AutoInclude();
            modelBuilder.Entity<Usuario>().Navigation(e => e.Domicilios).AutoInclude();


            modelBuilder.Entity<Producto>().Navigation(e => e.Categoria).AutoInclude();

            modelBuilder.Entity<Carrito>().Navigation(e => e.Usuario).AutoInclude();
            modelBuilder.Entity<Carrito>().Navigation(e => e.Productos).AutoInclude();

            modelBuilder.Entity<CarritoProducto>().Navigation(e => e.Producto).AutoInclude();
            modelBuilder.Entity<CarritoProducto>().Navigation(e => e.Carrito).AutoInclude();

            modelBuilder.Entity<Favorito>().Navigation(e => e.Producto).AutoInclude();
            modelBuilder.Entity<Favorito>().Navigation(e => e.Usuario).AutoInclude();

            modelBuilder.Entity<Orden>().Navigation(e => e.Carrito).AutoInclude();
            modelBuilder.Entity<Orden>().Navigation(e => e.Usuario).AutoInclude();

            base.OnModelCreating(modelBuilder);
        }
    }
}
