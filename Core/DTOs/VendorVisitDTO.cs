namespace Core.DTOs
{
    public class VendorVisitDTO
    {
        public int VendorId { get; set; }
        public VendorDTO Vendor { get; set; }
        public List<StockItemDTO> StockItems { get; set; }
    }
}
