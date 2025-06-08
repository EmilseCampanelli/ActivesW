using APIAUTH.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<ColorTheme> ColorThemes { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuChild> MenuItems { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Navigation(e => e.Role).AutoInclude();
            modelBuilder.Entity<User>().Navigation(e => e.Company).AutoInclude();
            modelBuilder.Entity<User>().Navigation(e => e.Account).AutoInclude();
            modelBuilder.Entity<User>().Navigation(e => e.Address).AutoInclude();


            modelBuilder.Entity<Product>().Navigation(e => e.Category).AutoInclude();
            modelBuilder.Entity<Product>().Navigation(e => e.Favorites).AutoInclude();

            modelBuilder.Entity<Favorite>().Navigation(e => e.Product).AutoInclude();
            modelBuilder.Entity<Favorite>().Navigation(e => e.User).AutoInclude();

            modelBuilder.Entity<Orden>().Navigation(e => e.User).AutoInclude();

            modelBuilder.Entity<Category>()
               .HasMany(c => c.SubCategory)
               .WithOne(c => c.ParentCategory)
               .HasForeignKey(c => c.ParentCategoryId)
               .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<ColorTheme>().HasData(
                            new ColorTheme { Id = 1, Mode = "light", Section = "primary", Property = "main", Value = "#FFFFFF" },
                            new ColorTheme { Id = 2, Mode = "light", Section = "primary", Property = "contrastText", Value = "#000000" },
                            new ColorTheme { Id = 3, Mode = "light", Section = "secondary", Property = "main", Value = "#000000" },
                            new ColorTheme { Id = 4, Mode = "light", Section = "secondary", Property = "contrastText", Value = "#FFFFFF" },
                            new ColorTheme { Id = 5, Mode = "light", Section = "background", Property = "default", Value = "#FFFFFF" },
                            new ColorTheme { Id = 6, Mode = "light", Section = "background", Property = "paper", Value = "#F9F9F9" },
                            new ColorTheme { Id = 7, Mode = "light", Section = "text", Property = "primary", Value = "#000000" },
                            new ColorTheme { Id = 8, Mode = "light", Section = "text", Property = "secondary", Value = "#666666" },
                            new ColorTheme { Id = 9, Mode = "light", Section = "text", Property = "disabled", Value = "#BBBBBB" },
                            new ColorTheme { Id = 10, Mode = "light", Section = "border", Property = "main", Value = "#DDDDDD" },
                            new ColorTheme { Id = 11, Mode = "light", Section = "success", Property = "main", Value = "#4CAF50" },
                            new ColorTheme { Id = 12, Mode = "light", Section = "success", Property = "contrastText", Value = "#FFFFFF" },
                            new ColorTheme { Id = 13, Mode = "light", Section = "warning", Property = "main", Value = "#FFC107" },
                            new ColorTheme { Id = 14, Mode = "light", Section = "warning", Property = "contrastText", Value = "#000000" },
                            new ColorTheme { Id = 15, Mode = "light", Section = "error", Property = "main", Value = "#F44336" },
                            new ColorTheme { Id = 16, Mode = "light", Section = "error", Property = "contrastText", Value = "#FFFFFF" },
                            new ColorTheme { Id = 17, Mode = "dark", Section = "primary", Property = "main", Value = "#000000" },
                            new ColorTheme { Id = 18, Mode = "dark", Section = "primary", Property = "contrastText", Value = "#FFFFFF" },
                            new ColorTheme { Id = 19, Mode = "dark", Section = "secondary", Property = "main", Value = "#FFFFFF" },
                            new ColorTheme { Id = 20, Mode = "dark", Section = "secondary", Property = "contrastText", Value = "#000000" },
                            new ColorTheme { Id = 21, Mode = "dark", Section = "background", Property = "default", Value = "#121212" },
                            new ColorTheme { Id = 22, Mode = "dark", Section = "background", Property = "paper", Value = "#1E1E1E" },
                            new ColorTheme { Id = 23, Mode = "dark", Section = "text", Property = "primary", Value = "#FFFFFF" },
                            new ColorTheme { Id = 24, Mode = "dark", Section = "text", Property = "secondary", Value = "#AAAAAA" },
                            new ColorTheme { Id = 25, Mode = "dark", Section = "text", Property = "disabled", Value = "#555555" },
                            new ColorTheme { Id = 26, Mode = "dark", Section = "border", Property = "main", Value = "#333333" },
                            new ColorTheme { Id = 27, Mode = "dark", Section = "success", Property = "main", Value = "#81C784" },
                            new ColorTheme { Id = 28, Mode = "dark", Section = "success", Property = "contrastText", Value = "#000000" },
                            new ColorTheme { Id = 29, Mode = "dark", Section = "warning", Property = "main", Value = "#FFD54F" },
                            new ColorTheme { Id = 30, Mode = "dark", Section = "warning", Property = "contrastText", Value = "#000000" },
                            new ColorTheme { Id = 31, Mode = "dark", Section = "error", Property = "main", Value = "#E57373" },
                            new ColorTheme { Id = 32, Mode = "dark", Section = "error", Property = "contrastText", Value = "#000000" }
                        );
            modelBuilder.Entity<Menu>().HasData(
                           new Menu { Id = 1, Label = "Dashboard", Path = "/dashboard", Icon = "dashboard_icon" },
                           new Menu { Id = 2, Label = "Products", Path = "/products", Icon = "products_icon" },
                           new Menu { Id = 3, Label = "Users", Path = "/users", Icon = "users_icon" }
                       );

            modelBuilder.Entity<MenuChild>().HasData(
                            new MenuChild { Id = 1, Label = "Product List", Path = "/products/list", MenuId = 2 },
                            new MenuChild { Id = 2, Label = "Add Product", Path = "/products/add", MenuId = 2 },
                            new MenuChild { Id = 3, Label = "User List", Path = "/users/list", MenuId = 3 },
                            new MenuChild { Id = 4, Label = "Add User", Path = "/users/add", MenuId = 3 }
                        );

            base.OnModelCreating(modelBuilder);
        }
    }
}
