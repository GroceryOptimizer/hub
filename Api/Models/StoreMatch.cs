using Core.Entities;

namespace Api.Models;

public class StoreMatch
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public List<StockItem> StockItems { get; set; } = [];
}
