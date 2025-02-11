using Core.Entities;

namespace Api.Models;

public record CartReq
{
    public List<CartItem> Items { get; set; } = [];
}
