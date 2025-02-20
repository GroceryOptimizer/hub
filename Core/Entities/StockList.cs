namespace Core.Entities
{
    public class StockList
    {
        public int Id { get; set; }
        public List<StockItem> StockItems { get; set; } = new List<StockItem>();
    }
}