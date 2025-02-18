using Core.Entities;

using Data;

using Grpc.Core;

using StoreProto;

namespace StoreApi.Services;

public class HubServer : HubService.HubServiceBase
{
    private readonly ApplicationDbContext _context;

    public HubServer(ApplicationDbContext context)
    {
        _context = context;
    }

    public override async Task<HandShakeResponse> HandShake(
        HandShakeRequest request,
        ServerCallContext context
    )
    {
        var store = MapToStore(request.Store);

        _context.Stores.Add(store);
        await _context.SaveChangesAsync();

        Console.WriteLine("Received handshake from Go Store service: " + request.Store);

        return new HandShakeResponse { Id = store.Id.ToString() };
    }

    public override async Task<UpdateInventoryResponse> UpdateInventory(
        UpdateInventoryRequest request,
        ServerCallContext context
    )
    {
        var store = await _context.Stores.FindAsync(Guid.Parse(request.StoreId)) ??
            throw new RpcException(new Status(StatusCode.NotFound, "Store not found"));

        var productNames = _context.Products.Select(p => p.Name);
        var stockItemNames = request.StockItems.Select(x => x.Product.Name);
        //var products = await _context.Product.Where(p => stockItemNames.Contains(p.Name)).ToListAsync();
        var query = _context.Products.Where(p => stockItemNames.Contains(p.Name));
        var products = await query.ToListAsync();

        var storeInventories = new List<StoreInventory>();

        foreach (var stockItem in request.StockItems)
        {
            //var product = Products.First(p => p.Name == stockItem.Product.Name);
            //storeInventories.Add(MapToStoreInventory(stockItem, product, store));
        }


        _context.Inventories.AddRange(storeInventories);
        var nrOfMutations = await _context.SaveChangesAsync();

        Console.WriteLine($"Received inventory list from Go Store service: {nrOfMutations} items");

        return new UpdateInventoryResponse { Message = "received inventory list" };
    }

    private static Core.Entities.Store MapToStore(StoreProto.Store store) =>
        new()
        {
            Name = store.Name,
            GrpcAddress = store.GrpcAddress,
            Location = new()
            {
                Latitude = store.Location.Latitude,
                Longitude = store.Location.Longitude,
            },
        };

    private static StoreInventory MapToStoreInventory(StoreProto.StockItem stockItem, Core.Entities.Product product, Core.Entities.Store store)
        => new() {
            StoreId = store.Id,
            ProductId = product.Id,
            Price = stockItem.Price,
            Quantity = stockItem.Quantity,
        };
}