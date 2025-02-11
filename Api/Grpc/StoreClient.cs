using Core.Entities;
using Grpc.Net.Client;
using Microsoft.EntityFrameworkCore;
using StoreProto;

namespace Api.Grpc;

public class StoreClient : IStoreClient
{
    private readonly IStoreRepository _storeRepo;

    public StoreClient(IStoreRepository storeRepository)
    {
        _storeRepo = storeRepository;
    }

    public async Task<List<StoreMatch>> GetStoresByStock(Core.Entities.StockItem[] items)
    {
        var stores = await _storeRepo.Query().Where(x => x.GrpcAdress != null).ToListAsync();

        var matches = new List<StoreMatch>();
        foreach (var store in stores)
        {
            try
            {
                using var channel = GrpcChannel.ForAddress(store.GrpcAdress);
                var client = new StoreService.StoreServiceClient(channel);
                var req = new InventoryRequest { ShoppingCart = { items.Select(MapProtoProduct) } };
                var res = await client.ProductsAsync(req);

                if (res.StockItems.Count != 0)
                    matches.Add(CreateStoreMatch(store.Id, store.Name, res.StockItems));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing store {store.Name}: {ex.Message}");
            }
        }

        return matches;
    }

    /* Helpers
    ****************************************************/

    private static StoreMatch CreateStoreMatch(
        Guid id,
        string name,
        Google.Protobuf.Collections.RepeatedField<StoreProto.StockItem> items
    ) =>
        new()
        {
            Id = id,
            Name = name,
            StockItems = [.. items.Select(MapStockItem)],
        };

    private static StoreProto.Product MapProtoProduct(Core.Entities.StockItem x) =>
        new() { Name = x.Product.Name };

    private static Core.Entities.Product MapProduct(StoreProto.Product x) => new(x.Name);

    private static Core.Entities.StockItem MapStockItem(StoreProto.StockItem x) =>
        new(new Core.Entities.Product(x.Name), 0);
}
