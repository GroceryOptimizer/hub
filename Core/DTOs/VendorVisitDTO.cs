namespace Core.DTOs
{
    public record VendorVisitDTO(int VendorId, VendorDTO Vendor, List<StockItemDTO> StockItems);
}
