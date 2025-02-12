using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[ComplexType]
public class Coordinates
{
    public float Latitude { get; set; }
    public float Longitude { get; set; }
}
