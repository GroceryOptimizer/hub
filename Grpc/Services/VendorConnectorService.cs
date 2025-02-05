using Grpc.Net.Client;

using VendorProto;

namespace Api.Services
{
    public class VendorConnectorService : IVendorConnectorService
    {
        private readonly VendorService.VendorServiceClient _client;

        public VendorConnectorService()
        {
            // todo: maybe accept port as parameter
            var channel = GrpcChannel.ForAddress("http://localhost:50051");
            _client = new VendorService.VendorServiceClient(channel);
        }

        public async Task<string> SendMessageAsync(string message)
        {
            var request = new SendMessageRequest { Message = message };
            var reply = await _client.SendMessageAsync(request);
            return reply.Reply;
        }

        /*
        public async Task<List<StockItemDTO>> GetInventoryAsync(ShoppingCartDTO cart)
        {
            var inventoryRequest = new InventoryRequest();

            foreach (var product in cart.Products)
            {
                inventoryRequest.ShoppingCart.Add(new Product { Name = product.Name });
            }

            var response = await _client.ProductsAsync(inventoryRequest);

            return response.StockItems
                .Select(item => new StockItemDTO(new ProductDTO(item.Name), item.Price))
                .ToList();
        }
        */
    }
}
