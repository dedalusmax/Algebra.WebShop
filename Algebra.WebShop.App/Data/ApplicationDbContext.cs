using Algebra.WebShop.App.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Algebra.WebShop.App.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<ProductCategory> ProductCategories { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Category>()
            .HasIndex(_ => _.Name)
            .IsUnique();

        builder.Entity<Product>()
            .HasIndex(_ => _.Name)
            .IsUnique();

        builder.Entity<OrderItem>()
            .HasIndex(_ => new { _.OrderId, _.ProductId })
            .IsUnique();

        base.OnModelCreating(builder);
    }
}
