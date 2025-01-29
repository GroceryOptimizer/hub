namespace Core.DTOs
{
    public class StockItemDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Amount { get; set; }
    }
}
