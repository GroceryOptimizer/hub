using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Core.Entities;

public class Store
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string GrpcAddress { get; set; } // http://1.2.3.4:5789

    // Navigation
    [Required]
    public Coordinates Location { get; set; } = new();
}