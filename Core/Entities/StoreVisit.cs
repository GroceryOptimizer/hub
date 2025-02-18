namespace Core.Entities
{
    public class StoreVisit
    {
        public int StoreId { get; set; }  // Foreign key
        public Store Store { get; set; } = null;
        public List<StockItem> StockItems { get; set; }

        public StoreVisit(Store store, List<StockItem> stockItems)
        {
            Store = store;
            StockItems = stockItems;
        }
    }
}
