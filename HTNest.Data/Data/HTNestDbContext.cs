using HTNest.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Data.Data
{
    public class HTNestDbContext : DbContext
    {
        public HTNestDbContext() { }

        public HTNestDbContext(DbContextOptions<HTNestDbContext> options) : base(options)
        {
        }

        public DbSet<Role> Role {  get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Style> Style { get; set; }
        public DbSet<Product> Product { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Role to User Relationship
            modelBuilder.Entity<Role>()
                .HasMany(r => r.User)  // One Role has many Users
                .WithOne(u => u.Role)   // One User belongs to one Role
                .HasForeignKey(u => u.RoleId); // Foreign key in User

            // Category to Product Relationship
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Product)  // One Category has many Products
                .WithOne(p => p.Category)  // One Product belongs to one Category
                .HasForeignKey(p => p.CategoryId); // Foreign key in Product

            // Style to Product Relationship
            modelBuilder.Entity<Style>()
                .HasMany(s => s.Products)  // One Style has many Products
                .WithOne(p => p.Style)     // One Product belongs to one Style
                .HasForeignKey(p => p.StyleId); // Foreign key in Product

            // User to Feedback Relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Feedbacks)  // One User can give many Feedbacks
                .WithOne(f => f.User)       // One Feedback belongs to one User
                .HasForeignKey(f => f.UserName); // Foreign key in Feedback

            // Product to Feedback Relationship
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Feedbacks)  // One Product can have many Feedbacks
                .WithOne(f => f.Product)    // One Feedback is for one Product
                .HasForeignKey(f => f.ProductId); // Foreign key in Feedback

            // Product to OrderDetail Relationship
            modelBuilder.Entity<Product>()
                .HasMany(p => p.OrderDetail) // One Product can be in many OrderDetails
                .WithOne(od => od.Product)    // One OrderDetail is for one Product
                .HasForeignKey(od => od.ProductId); // Foreign key in OrderDetail

            // Order to OrderDetail Relationship
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderDetail)  // One Order has many OrderDetails
                .WithOne(od => od.Order)       // One OrderDetail belongs to one Order
                .HasForeignKey(od => od.OrderId); // Foreign key in OrderDetail

            // User to Order Relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Order)  // One User can place many Orders
                .WithOne(o => o.User)    // One Order belongs to one User
                .HasForeignKey(o => o.UserName); // Foreign key in Order

            // Optional: Set additional constraints, e.g., cascading deletes, unique indexes, etc.
        }

    }
}
