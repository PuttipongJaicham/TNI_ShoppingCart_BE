using Microsoft.EntityFrameworkCore;
using ShoppingCart.Model;

namespace ShoppingCart.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Stock> Stocks => Set<Stock>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");
            entity.HasKey(product => product.id);
            entity.Property(product => product.id).ValueGeneratedOnAdd();
            entity.Property(product => product.name).IsRequired();
            entity.Property(product => product.price).IsRequired();
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.ToTable("Stock");
            entity.HasKey(stock => stock.id);
            entity.Property(stock => stock.id).ValueGeneratedOnAdd();
            entity.Property(stock => stock.productId).IsRequired();
            entity.Property(stock => stock.total).IsRequired();
        });
    }
}
