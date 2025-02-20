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
        Console.WriteLine("Received inventory list from Go Store service: {0}, {1} ", request.StockItems);

        // Get the existing stock list from DB
        var existingStockList = _context.StockList.FirstOrDefault() ?? new Core.Entities.StockList();

        // Map gRPC request to StockList
        var newStockList = MapToStockList(request);

        // Merge existing and new stock items (avoid duplicates)
        foreach (var newItem in newStockList.StockItems)
        {
            var existingItem = existingStockList.StockItems.FirstOrDefault(s => s.Product.Name == newItem.Product.Name);

            if (existingItem == null)
            {
                // If product is not in the list, add it
                existingStockList.StockItems.Add(newItem);
            }
        }

        // Save updated stock list to DB
        _context.StockList.Update(existingStockList);
        await _context.SaveChangesAsync();

        return new UpdateInventoryResponse { Message = "Stock updated successfully" };
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

    private static Core.Entities.StockList MapToStockList(StoreProto.UpdateInventoryRequest stockItems)
    {
        Core.Entities.StockList stockList = new Core.Entities.StockList();

        foreach (var item in stockItems.StockItems)
        {
            Core.Entities.Product newProduct = new Core.Entities.Product(item.Product.Name);
            Core.Entities.StockItem newStockItem = new Core.Entities.StockItem(newProduct, item.Quantity, item.Price);
            stockList.StockItems.Add(newStockItem);
        }

        return stockList;
    }
}