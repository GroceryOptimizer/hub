using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[ComplexType]
public class ProductPackage
{
    public decimal? Quantity { get; set; }
    public string? Unit { get; set; }
    public string? Type { get; set; }
}
