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
        Console.WriteLine("Handshake request in:");
        Console.WriteLine("Store name: " + request.Store.Name);
        Console.WriteLine("Store address: " + request.Store.GrpcAddress);
        Console.WriteLine("Store location: " + request.Store.Location);
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
}
