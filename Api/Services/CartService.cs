using Api.Grpc;
using Core.Entities;

namespace Api.Services;

public class CartService : ICartService
{
    private readonly IStoreClient _storeClient;
    private readonly IStoreRepository _storeRepo;

    public CartService(IStoreRepository storRepo)
    {
        _storeRepo = storRepo;
        _storeClient = new StoreClient(_storeRepo);
    }

    public async Task<List<StoreMatch>> GetStoreByCart(List<CartItem> cart)
    {
        var matches = await _storeClient.GetStoresByStock(
            [.. cart.Select(x => new StockItem(x.Product, 0))]
        );

        return matches;
    }
}
