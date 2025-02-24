using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Data;

public static class ProductSeed
{
    public static void SeedProducts(this ModelBuilder mb)
    {
        var json = File.ReadAllText("../Data/ProductSeed.json");
        if (string.IsNullOrWhiteSpace(json))
            return;

        var products = JsonConvert.DeserializeObject<List<Product>>(json);
        products?.ForEach(p => mb.Entity<Product>().HasData(p));
    }
}
