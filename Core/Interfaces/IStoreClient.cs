namespace Core.Interfaces;

public interface IStoreClient
{
    Task<List<StoreMatch>> GetStoresByStock(StockItem[] items);
}
