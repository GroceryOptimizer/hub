using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Core.Entities;

public class Vendor
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    public string GrpcAddress { get; set; } // http://1.2.3.4:5789

    // Navigation
    [Required]
    public Coordinates Location { get; set; } = new();
}
