using Microsoft.EntityFrameworkCore;
using APIAUTH.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace APIAUTH.Data.Context
{
    public class ActivesWContext : DbContext
    {
        public ActivesWContext(DbContextOptions<ActivesWContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cuenta> Users { get; set; }
        public DbSet<Empresa> Companies { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UsuarioTipo> UsersType { get; set; }
        public DbSet<Notificacion> Notifications { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<Domicilio> Domicilios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().Navigation(e => e.Role).AutoInclude();
            modelBuilder.Entity<Usuario>().Navigation(e => e.Company).AutoInclude();
            modelBuilder.Entity<Usuario>().Navigation(e => e.User).AutoInclude();
            modelBuilder.Entity<Usuario>().Navigation(e => e.UserType).AutoInclude();
            modelBuilder.Entity<Usuario>().Navigation(e => e.Domicilios).AutoInclude();

            base.OnModelCreating(modelBuilder);
        }
    }
}
