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
        var vendor = MapToVendor(request.Store);

        _context.Vendors.Add(vendor);
        await _context.SaveChangesAsync();

        Console.WriteLine("Received handshake from Go Vendor service: " + request.Store);

        return new HandShakeResponse { Id = vendor.Id.ToString() };
    }

    private static Vendor MapToVendor(Store store) =>
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
