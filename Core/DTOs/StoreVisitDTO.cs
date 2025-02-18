namespace Core.DTOs
{
    public record StoreVisitDTO(int StoreId, StoreDTO Store, List<StockItemDTO> StockItems);
}
