using Microsoft.EntityFrameworkCore;
using APIAUTH.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace APIAUTH.Data.Context
{
    public class ActivesWContext : DbContext
    {
        public ActivesWContext(DbContextOptions<ActivesWContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Notificacion> Notifications { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductLine> ProductLines { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Navigation(e => e.Role).AutoInclude();
            modelBuilder.Entity<User>().Navigation(e => e.Company).AutoInclude();
            modelBuilder.Entity<User>().Navigation(e => e.Account).AutoInclude();
            modelBuilder.Entity<User>().Navigation(e => e.Address).AutoInclude();


            modelBuilder.Entity<Product>().Navigation(e => e.Category).AutoInclude();

            modelBuilder.Entity<Favorite>().Navigation(e => e.Product).AutoInclude();
            modelBuilder.Entity<Favorite>().Navigation(e => e.User).AutoInclude();

            modelBuilder.Entity<Orden>().Navigation(e => e.User).AutoInclude();

            modelBuilder.Entity<Category>()
               .HasMany(c => c.SubCategory)
               .WithOne(c => c.ParentCategory)
               .HasForeignKey(c => c.ParentCategoryId)
               .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
