namespace Core.Interfaces;

public interface IStoreRepository
{
    Task<List<Store>> GetAllStores();
    Task<int> AddStore(Store store);
}
