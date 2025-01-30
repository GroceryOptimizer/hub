namespace Core.Entities
{
    public class VendorVisit
    {
        public int VendorId { get; set; }  // Foreign key
        public Vendor Vendor { get; set; } = null;
        public List<StockItem> StockItems { get; set; }

        public VendorVisit(Vendor vendor, List<StockItem> stockItems)
        {
            Vendor = vendor;
            StockItems = stockItems;
        }
    }
}
