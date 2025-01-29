using Core.Entities;

using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<StockItem> StockItems { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<VendorVisit> VendorVisits { get; set; }
        public DbSet<Coordinates> Coordinates { get; set; }
    }
}
