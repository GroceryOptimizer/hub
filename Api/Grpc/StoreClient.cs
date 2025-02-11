namespace Api.Grpc;

using StoreProto;

public class StoreClient : IStoreClient
{
    private readonly IStoreRepository _storeRepo;

    public StoreClient(IStoreRepository storeRepository)
    {
        _storeRepo = storeRepository;
    }

    public async Task<List<StoreMatch>> GetStoresByStock(StockItem[] items)
    {
        throw new NotImplementedException();
    }
}
