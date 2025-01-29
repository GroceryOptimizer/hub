using System;
using System.Threading.Tasks;

using Grpc.Net.Client;

using VendorProto;  // The namespace we specified in the proto file

namespace HubClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // The server is listening on localhost:50051
            using var channel = GrpcChannel.ForAddress("http://localhost:50051");

            // The generated client is VendorService.VendorServiceClient
            var client = new VendorService.VendorServiceClient(channel);

            Console.WriteLine("Sending message to Go Vendor service...");
            var request = new SendMessageRequest { Message = "Hello from C# client!" };

            // Perform the gRPC call
            var reply = await client.SendMessageAsync(request);

            Console.WriteLine("Received reply: " + reply.Reply);


            var inventoryClient = new VendorService.VendorServiceClient(channel);
            var shoppingCart = new InventoryRequest();
            shoppingCart.ShoppingCart.Add(new Product { Name = "Milk" });
            shoppingCart.ShoppingCart.Add(new Product { Name = "Bread" });
            shoppingCart.ShoppingCart.Add(new Product { Name = "Eggs" });

            var shoppingReply =await inventoryClient.ProductsAsync(shoppingCart);

            Console.WriteLine("Recieved inventory reply with {0} items", shoppingReply.StockItems.Count);
            foreach (var item in shoppingReply.StockItems)
            {
                Console.WriteLine(item.Name + ", costs: " + item.Price);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
