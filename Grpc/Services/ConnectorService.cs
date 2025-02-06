using Core.DTOs;

using Grpc.Net.Client;

using StoreProto;

namespace Api.Services
{
    public class ConnectorService : IConnectorService
    {
        private readonly StoreService.StoreServiceClient _client;

        public ConnectorService()
        {
            // todo: maybe accept port as parameter
            var channel = GrpcChannel.ForAddress("http://localhost:50051");
            _client = new StoreService.StoreServiceClient(channel);
        }

        public async Task<string> SendMessageAsync(string message)
        {
            var request = new SendMessageRequest { Message = message };
            var reply = await _client.SendMessageAsync(request);
            return reply.Reply;
        }


        public async Task<List<StockItemDTO>> GetInventoryAsync()//(ShoppingCartDTO cart)
        {
            var inventoryRequest = new InventoryRequest();

            Console.WriteLine("building cart..");

            /*
            foreach (var product in cart.Cart)
            {
                inventoryRequest.ShoppingCart.Add(new VendorProto.Product { Name = product.Name });
            }
            */
            // temp cart building
            inventoryRequest.ShoppingCart.Add(new Product { Name = "Milk" });
            inventoryRequest.ShoppingCart.Add(new Product { Name = "Bread" });
            inventoryRequest.ShoppingCart.Add(new Product { Name = "Eggs" });

            Console.WriteLine(inventoryRequest.ShoppingCart);
            Console.WriteLine("cart built..");
            Console.WriteLine("sending request..");
            var response = await _client.ProductsAsync(inventoryRequest);
            Console.WriteLine("response recieved..");
            Console.WriteLine("print entire response..");
            Console.WriteLine(response);
            Console.WriteLine("print response.StockItems..");
            Console.WriteLine(response.StockItems);
            Console.WriteLine("printing through foreach..");
            foreach (var item in response.StockItems)
            {
                Console.WriteLine("Name: " + item.Name + ", Price: " + item.Price);
            }
            Console.WriteLine("returning from GetInventoryAsync");

            return response.StockItems
                .Select(item => new StockItemDTO(new ProductDTO(item.Name), item.Price))
                .ToList();
        }

    }
}
