namespace Core.Interfaces;

public interface IStoreRepository
{
    IQueryable<Store> Query();
    Task<List<Store>> GetAllStores();
    Task<int> Add(Store store);
}
