using Core.Entities;

using Data;

using Grpc.Core;

using StoreProto;

using Microsoft.EntityFrameworkCore;

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
        Console.WriteLine("Received inventory list from Go Store service: {0}, {1} ", request.StockItems, request.StoreId);
        var store = await _context.Stores.FindAsync(Guid.Parse(request.StoreId)) ??
            throw new RpcException(new Status(StatusCode.NotFound, "Store not found"));

        // Get all product names from the database
        //var productNames = _context.Products.Select(p => p.Name);
        // Get all product names from the request
        //var stockItemNames = request.StockItems.Select(x => x.Product.Name);
        // Get all products that are not in the database
        //var products = stockItemNames.Where(p => !productNames.Contains(p)).ToList();

        var storeInventories = new List<StoreInventory>();

        // Map the stock items to store inventories

        foreach (var stockItem in request.StockItems)
        {
            var product = _context.Products.FirstOrDefault(p => p.Name == stockItem.Product.Name) ??
                new Core.Entities.Product { Name = stockItem.Product.Name };

            if (!_context.Products.Contains(product))
            {
                _context.Products.Add(product);
                storeInventories.Add(MapToStoreInventory(stockItem, product, store));
            }
            else
            {
                Console.WriteLine("Product already exists in the database {0}", product.Name);
            }

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