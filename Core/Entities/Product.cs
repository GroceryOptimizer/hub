using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

#nullable disable

public class Product
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string SKU { get; set; }

    [Required]
    public ProductPackage Package { get; set; } = new();
}
