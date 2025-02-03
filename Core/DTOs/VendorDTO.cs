namespace Core.DTOs
{
    public record VendorDTO(int Id, string Name, int CoordinatesId, CoordinatesDTO Coordinates);
}
