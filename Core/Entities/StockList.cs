namespace Core.Entities
{
    public class StockList
    {
        public List<StockItem> StockItems { get; set; } = new List<StockItem>();

        public StockList(List<StockItem> stockList)
        {
            this.StockItems = stockList;
        }
    }
}