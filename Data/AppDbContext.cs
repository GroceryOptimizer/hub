using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Store> Stores { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<StoreInventory> Inventories { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.SeedProducts();
    }
}
