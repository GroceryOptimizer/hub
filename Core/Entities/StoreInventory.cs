using Microsoft.EntityFrameworkCore;

namespace Core.Entities;
#nullable disable

[PrimaryKey(nameof(StoreId), nameof(ProductId))]
public class StoreInventory
{
    public Guid StoreId { get; set; }  // Foreign key

    public Guid ProductId { get; set; }  // Foreign key

    public int Price { get; set; }

    public int Quantity { get; set; }

    public Product Product { get; set; }

    public Store Store { get; set; }
}