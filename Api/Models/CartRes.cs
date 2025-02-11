using Core.Entities;

namespace Api.Models;

public record CartRes
{
    public List<StoreMatch> StoreMatches { get; set; } = [];
}
