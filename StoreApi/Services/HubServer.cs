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