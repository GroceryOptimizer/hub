namespace Core.DTOs
{
    public record StockItemDTO(ProductDTO Product, int Price, int Quantity);
}