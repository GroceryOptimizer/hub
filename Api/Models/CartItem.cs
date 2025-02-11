using Core.Entities;

namespace Api.Models;

public class CartItem
{
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }
}
