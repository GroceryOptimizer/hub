﻿using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Store> Stores { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<StoreInventory> Inventories { get; set; }
}
