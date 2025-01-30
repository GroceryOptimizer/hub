namespace Core.DTOs
{
    public class ShoppingCartDTO
    {
        public int Id { get; set; }
        public List<StockItemDTO> Cart { get; set; }
    }
}
