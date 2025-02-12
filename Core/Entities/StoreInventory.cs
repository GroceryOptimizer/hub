using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Core.Entities;

#nullable disable

[PrimaryKey(nameof(StoreId), nameof(ProductId))]
public class StoreStock
{
    public Guid StoreId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public int Price { get; set; }

    // Navigation properties
    public virtual Store Store { get; set; }
    public virtual Product Product { get; set; }
}
