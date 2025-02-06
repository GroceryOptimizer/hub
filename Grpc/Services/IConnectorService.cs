using Core.DTOs;

namespace Api.Services
{
    public interface IConnectorService
    {
        Task<string> SendMessageAsync(string message);
        Task<List<StockItemDTO>> GetInventoryAsync();
        //Task<List<StockItemDTO>> GetInventoryAsync(ShoppingCartDTO cart);
    }
}
