using Data;
using Grocer.Grpc;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace Api.Grpc;

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
        var store =
            await _context.Stores.FindAsync(Guid.Parse(request.StoreId))
            ?? throw new RpcException(new Status(StatusCode.NotFound, "Store not found"));

        // Get all product names from the database
        var productNames = _context.Products.Select(p => p.Name);
        // Get all product names from the request
        var stockItemNames = request.StockItems.Select(x => x.Product.Name);
        // Get all products that are not in the request
        var products = await _context
            .Products.Where(p => !stockItemNames.Contains(p.Name))
            .ToListAsync();

        var storeInventories = new List<StoreInventory>();

        // Map the stock items to store inventories
        foreach (var stockItem in request.StockItems)
        {
            var product = products.First(p => p.Name == stockItem.Product.Name);
            var map = MapToStoreInventory(stockItem, product, store);

            storeInventories.Add(map);
        }

        _context.Inventories.AddRange(storeInventories);
        var nrOfMutations = await _context.SaveChangesAsync();

        Console.WriteLine($"Received inventory list from Go Store service: {nrOfMutations} items");

        return new UpdateInventoryResponse { Message = "received inventory list" };
    }

    private static Core.Entities.Store MapToStore(Grocer.Grpc.Store store) =>
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

    private static StoreInventory MapToStoreInventory(
        Grocer.Grpc.StockItem stockItem,
        Core.Entities.Product product,
        Core.Entities.Store store
    ) =>
        new()
        {
            StoreId = store.Id,
            ProductId = product.Id,
            Price = stockItem.Price,
            Quantity = stockItem.Quantity,
        };
}
