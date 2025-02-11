using Grpc.Core;
using StoreProto;

namespace Api.Grpc;

public class HubServer : HubService.HubServiceBase
{
    private readonly IStoreRepository _storeRepo;

    public HubServer(IStoreRepository storeRepository)
    {
        _storeRepo = storeRepository;
    }

    public override async Task<HandShakeResponse> HandShake(
        HandShakeRequest request,
        ServerCallContext context
    )
    {
        var store = ParseStore(request);
        await _storeRepo.Add(store);
        return new() { Id = store.Id.ToString() };
    }

    private static Core.Entities.Store ParseStore(HandShakeRequest req)
    {
        Core.Entities.Store newStore = new()
        {
            Id = Guid.NewGuid(),
            Name = req.Store.Name,
            Location = null!,
            GrpcAdress = req.GrpcAddress,
        };

        return newStore;
    }
}
