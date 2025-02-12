using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

#nullable disable

public class Store
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public Coordinates Location { get; set; }

    public string GrpcAdress { get; set; }
}
