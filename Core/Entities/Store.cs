namespace Core.Entities;

public class Store
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public Coordinates Location { get; set; } = null!;
    public string GrpcAdress { get; set; } = null!;
}
