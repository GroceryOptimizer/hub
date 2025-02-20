using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

#nullable disable

public class Product
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; }
}
