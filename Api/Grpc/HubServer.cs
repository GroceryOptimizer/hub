using Grpc.Core;
using Microsoft.CodeAnalysis;
using StoreProto;

namespace Api.Grpc;

public class HubServer : HubService.HubServiceBase
{
    public HubServer() { }

    public override Task<HandShakeResponse> HandShake(
        HandShakeRequest request,
        ServerCallContext context
    )
    {
        var store = ParseStore(request);
        Console.WriteLine("Received handshake from Go Vendor service: " + request.Store);

        return Task.FromResult(new HandShakeResponse { Id = Guid.NewGuid().ToString() });
    }

    private static Core.Entities.Store ParseStore(HandShakeRequest req)
    {
        Core.Entities.Store newStore = new()
        {
            Id = Guid.NewGuid(),
            Name = req.Store.Name,
            Location = null!,
        };

        return newStore;
    }
}
