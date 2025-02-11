using Grpc.Core;

using StoreProto;

namespace Grpc.Services
{
    public class HubServer : HubService.HubServiceBase
    {
        public HubServer()
        {
        }

        public override Task<HandShakeResponse> HandShake(HandShakeRequest request, ServerCallContext context)
        {
            Console.WriteLine("Received handshake from Go Vendor service: " + request.Store);

            return Task.FromResult(new HandShakeResponse { Id = Guid.NewGuid().ToString()});
        }
    }
}