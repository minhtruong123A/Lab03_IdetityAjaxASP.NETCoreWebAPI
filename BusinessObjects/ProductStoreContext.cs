using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessObjects
{
    public partial class ProductStoreContext : DbContext
    {
        public ProductStoreContext()
        {
        }

        public ProductStoreContext(DbContextOptions<ProductStoreContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlServer(GetConnectionString());

        private string? GetConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", false, true).Build();
            return configuration["ConnectionStrings:MyStoreDB"];
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).ValueGeneratedOnAdd();
                entity.Property(e => e.CategoryName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Category>().HasData(
              new Category { CategoryId = 1, CategoryName = "Electronics" },
              new Category { CategoryId = 2, CategoryName = "Books" },
              new Category { CategoryId = 3, CategoryName = "Clothing" },
              new Category { CategoryId = 4, CategoryName = "Food" },
              new Category { CategoryId = 5, CategoryName = "Toys" },
              new Category { CategoryId = 6, CategoryName = "Furniture" },
              new Category { CategoryId = 7, CategoryName = "Sports" },
              new Category { CategoryId = 8, CategoryName = "Music" },
              new Category { CategoryId = 9, CategoryName = "Health" },
              new Category { CategoryId = 10, CategoryName = "Movies" }
             );

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).ValueGeneratedOnAdd();
                entity.Property(e => e.ProductName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, ProductName = "Laptop", CategoryId = 1, UnitsInStock = 50, UnitPrice = 1000m },
                new Product { ProductId = 2, ProductName = "Smartphone", CategoryId = 1, UnitsInStock = 100, UnitPrice = 800m },
                new Product { ProductId = 3, ProductName = "T-Shirt", CategoryId = 3, UnitsInStock = 200, UnitPrice = 20m },
                new Product { ProductId = 4, ProductName = "Pizza", CategoryId = 4, UnitsInStock = 30, UnitPrice = 15m },
                new Product { ProductId = 5, ProductName = "Novel", CategoryId = 2, UnitsInStock = 60, UnitPrice = 10m },
                new Product { ProductId = 6, ProductName = "Guitar", CategoryId = 8, UnitsInStock = 40, UnitPrice = 300m },
                new Product { ProductId = 7, ProductName = "Dumbbells", CategoryId = 7, UnitsInStock = 25, UnitPrice = 50m },
                new Product { ProductId = 8, ProductName = "Shampoo", CategoryId = 9, UnitsInStock = 150, UnitPrice = 5m },
                new Product { ProductId = 9, ProductName = "Movie DVD", CategoryId = 10, UnitsInStock = 80, UnitPrice = 12m },
                new Product { ProductId = 10, ProductName = "Sofa", CategoryId = 6, UnitsInStock = 20, UnitPrice = 500m }
             );

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.RoleName)
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, RoleName = "Admin" },
                new Role { Id = 2, RoleName = "User" }
            );

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Username)
                    .HasMaxLength(100);
                entity.Property(e => e.Password)
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<User>().HasData(
              new User { Id = 1, Username = "admin", Password = "1", RoleId = 1 },
              new User { Id = 2, Username = "user1", Password = "password1", RoleId = 2 },
              new User { Id = 3, Username = "user2", Password = "password2", RoleId = 2 },
              new User { Id = 4, Username = "user3", Password = "password3", RoleId = 2 },
              new User { Id = 5, Username = "user4", Password = "password4", RoleId = 2 },
              new User { Id = 6, Username = "user5", Password = "password5", RoleId = 2 },
              new User { Id = 7, Username = "user6", Password = "password6", RoleId = 2 },
              new User { Id = 8, Username = "user7", Password = "password7", RoleId = 2 },
              new User { Id = 9, Username = "user8", Password = "password8", RoleId = 2 },
              new User { Id = 10, Username = "user9", Password = "password9", RoleId = 2 }
          );

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
