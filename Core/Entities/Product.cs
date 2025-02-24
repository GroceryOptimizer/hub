using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

#nullable disable

public class Product
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Brand { get; set; }

    public string Category { get; set; }

    public string Description { get; set; }

    public string Sku { get; set; }

    public string Image { get; set; }
}
