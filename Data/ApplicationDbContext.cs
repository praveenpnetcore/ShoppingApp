using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Models;

namespace ShoppingApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DiscountSetting> DiscountSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics", Description = "Electronic gadgets and devices", IsActive = true },
                new Category { Id = 2, Name = "Clothing", Description = "Men's and Women's clothing", IsActive = true },
                new Category { Id = 3, Name = "Home & Kitchen", Description = "Home appliances and kitchen items", IsActive = true },
                new Category { Id = 4, Name = "Books", Description = "Books and educational materials", IsActive = true },
                new Category { Id = 5, Name = "Sports & Fitness", Description = "Sports equipment and fitness gear", IsActive = true }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Samsung Galaxy S23", Description = "Latest Samsung flagship smartphone with advanced camera and display", Price = 79999, ImageUrl = "https://images.unsplash.com/photo-1610945265064-0e34e5519bbf?w=400", CategoryId = 1, StockQuantity = 50, IsActive = true, CreatedDate = DateTime.Now },
                new Product { Id = 2, Name = "Apple iPhone 15 Pro", Description = "Premium iPhone with A17 Pro chip and titanium design", Price = 134900, ImageUrl = "https://images.unsplash.com/photo-1592750475338-74b7b21085ab?w=400", CategoryId = 1, StockQuantity = 30, IsActive = true, CreatedDate = DateTime.Now },
                new Product { Id = 3, Name = "Sony WH-1000XM5", Description = "Industry-leading noise cancelling wireless headphones", Price = 29990, ImageUrl = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=400", CategoryId = 1, StockQuantity = 100, IsActive = true, CreatedDate = DateTime.Now },
                new Product { Id = 4, Name = "Dell XPS 15 Laptop", Description = "Powerful laptop with Intel Core i7 and stunning OLED display", Price = 159990, ImageUrl = "https://images.unsplash.com/photo-1593642702821-c8da6771f0c6?w=400", CategoryId = 1, StockQuantity = 25, IsActive = true, CreatedDate = DateTime.Now },
                new Product { Id = 5, Name = "Men's Formal Shirt", Description = "Premium cotton formal shirt for office wear", Price = 1999, ImageUrl = "https://images.unsplash.com/photo-1596755094514-f87e34085b2c?w=400", CategoryId = 2, StockQuantity = 200, IsActive = true, CreatedDate = DateTime.Now },
                new Product { Id = 6, Name = "Women's Designer Saree", Description = "Elegant silk saree with traditional design", Price = 8999, ImageUrl = "https://images.unsplash.com/photo-1610030469983-98e550d6193c?w=400", CategoryId = 2, StockQuantity = 75, IsActive = true, CreatedDate = DateTime.Now },
                new Product { Id = 7, Name = "Men's Leather Jacket", Description = "Genuine leather jacket for stylish look", Price = 12999, ImageUrl = "https://images.unsplash.com/photo-1551028719-00167b16eac5?w=400", CategoryId = 2, StockQuantity = 40, IsActive = true, CreatedDate = DateTime.Now },
                new Product { Id = 8, Name = "LG Refrigerator 650L", Description = "Smart inverter refrigerator with multi airflow", Price = 75990, ImageUrl = "https://images.unsplash.com/photo-1571175443880-49e1d25b2bc5?w=400", CategoryId = 3, StockQuantity = 20, IsActive = true, CreatedDate = DateTime.Now },
                new Product { Id = 9, Name = "Philips Air Fryer", Description = "Healthy cooking with rapid air technology", Price = 9999, ImageUrl = "https://images.unsplash.com/photo-1585515320310-259814833e62?w=400", CategoryId = 3, StockQuantity = 60, IsActive = true, CreatedDate = DateTime.Now },
                new Product { Id = 10, Name = "Prestige Mixer Grinder", Description = "750W powerful motor with 3 stainless steel jars", Price = 4999, ImageUrl = "https://images.unsplash.com/photo-1570222094114-d054a817e56b?w=400", CategoryId = 3, StockQuantity = 80, IsActive = true, CreatedDate = DateTime.Now },
                new Product { Id = 11, Name = "Clean Code Book", Description = "A Handbook of Agile Software Craftsmanship by Robert C. Martin", Price = 599, ImageUrl = "https://images.unsplash.com/photo-1544716278-ca5e3f4abd8c?w=400", CategoryId = 4, StockQuantity = 150, IsActive = true, CreatedDate = DateTime.Now },
                new Product { Id = 12, Name = "The Psychology of Money", Description = "Timeless lessons on wealth, greed, and happiness", Price = 399, ImageUrl = "https://images.unsplash.com/photo-1512820790803-83ca734da794?w=400", CategoryId = 4, StockQuantity = 200, IsActive = true, CreatedDate = DateTime.Now },
                new Product { Id = 13, Name = "Yoga Mat Premium", Description = "6mm thick anti-slip yoga mat", Price = 1299, ImageUrl = "https://images.unsplash.com/photo-1601925260368-ae2f83cf8b7f?w=400", CategoryId = 5, StockQuantity = 120, IsActive = true, CreatedDate = DateTime.Now },
                new Product { Id = 14, Name = "Dumbbell Set 20kg", Description = "Adjustable dumbbell set for home gym", Price = 5999, ImageUrl = "https://images.unsplash.com/photo-1534438327276-14e5300c3a48?w=400", CategoryId = 5, StockQuantity = 50, IsActive = true, CreatedDate = DateTime.Now },
                new Product { Id = 15, Name = "Treadmill Pro", Description = "Foldable treadmill with digital display and multiple programs", Price = 45999, ImageUrl = "https://images.unsplash.com/photo-1576678927484-cc907957088c?w=400", CategoryId = 5, StockQuantity = 15, IsActive = true, CreatedDate = DateTime.Now }
            );

            // Seed Discount Settings
            modelBuilder.Entity<DiscountSetting>().HasData(
                new DiscountSetting
                {
                    Id = 1,
                    SettingName = "High Value Product Discount",
                    MinimumAmountThreshold = 5000,
                    DiscountPercentage = 10,
                    IsActive = true,
                    Description = "10% discount on products priced above ₹5000",
                    CreatedDate = DateTime.Now
                }
            );

            // Configure relationships
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
