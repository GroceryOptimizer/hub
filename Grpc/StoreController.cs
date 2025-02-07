using Grpc.Net.Client;

using StoreProto;  // The namespace we specified in the proto file

namespace HubClient
{
    public class StoreController
    {

        public static async Task Connect()
        {
            var channels = new[]
            {
                "http://localhost:50051",
                "http://localhost:50052",
                "http://localhost:50053"
            };

            Dictionary<int, List<StockItem>> vendorVisitReply = new Dictionary<int, List<StockItem>>();
            int placeholderVendorId = 0;

            foreach (var currentChannel in channels)
            {
                // Create new channel from current index in "_channels"
                using var channel = GrpcChannel.ForAddress(currentChannel);
                // The generated client is VendorService.VendorServiceClient
                var client = new StoreService.StoreServiceClient(channel);

                Console.WriteLine("Sending gRPC request");
                // Setup all variables needed for the gRPC call
                var inventoryClient = new StoreService.StoreServiceClient(channel);
                var shoppingCart = new InventoryRequest();
                int vendorId;

                // Craft our testing-call to send out to Vendors                
                shoppingCart.ShoppingCart.Add(new Product { Name = "Milk" });
                shoppingCart.ShoppingCart.Add(new Product { Name = "Bread" });
                shoppingCart.ShoppingCart.Add(new Product { Name = "Eggs" });

                // Perform the call
                var shoppingReply = await inventoryClient.ProductsAsync(shoppingCart);

                // Do something with the reply of the call

                //List of the StockItems for this particular VendorVisit
                List<StockItem> stockItemList = new List<StockItem>();

                vendorId = placeholderVendorId;
                Console.WriteLine("Recieved inventory reply with {0} items from " + vendorId, shoppingReply.StockItems.Count);
                foreach (var item in shoppingReply.StockItems)
                {
                    //Console.WriteLine(item.Name + ", costs: " + item.Price);
                    stockItemList.Add(item);
                }
                vendorVisitReply.Add(vendorId, stockItemList);
                placeholderVendorId++;

            }
            Console.WriteLine("All gRPC calls done, printing list of Vendor Visits: ");
            foreach (int key in vendorVisitReply.Keys)
            {
                Console.WriteLine("Vendor ID " + key);
                foreach (StockItem item in vendorVisitReply[key])
                {
                    Console.WriteLine(item.Name + ", Price: " + item.Price);
                }
            }


            //// The server is listening on localhost:50051
            //using var channel = GrpcChannel.ForAddress(_channel);

            //// The generated client is VendorService.VendorServiceClient
            //var client = new VendorService.VendorServiceClient(channel);

            //Console.WriteLine("Sending message to Go Vendor service...");
            //var request = new SendMessageRequest { Message = "Hello from C# client!" };

            //// Perform the gRPC call
            //var reply = await client.SendMessageAsync(request);

            //Console.WriteLine("Received reply: " + reply.Reply);


            //var inventoryClient = new VendorService.VendorServiceClient(channel);
            //var shoppingCart = new InventoryRequest();
            //shoppingCart.ShoppingCart.Add(new Product { Name = "Milk" });
            //shoppingCart.ShoppingCart.Add(new Product { Name = "Bread" });
            //shoppingCart.ShoppingCart.Add(new Product { Name = "Eggs" });

            //var shoppingReply =await inventoryClient.ProductsAsync(shoppingCart);

            //Console.WriteLine("Recieved inventory reply with {0} items", shoppingReply.StockItems.Count);
            //foreach (var item in shoppingReply.StockItems)
            //{
            //    Console.WriteLine(item.Name + ", costs: " + item.Price);
            //}

            //Console.WriteLine("Press any key to exit...");
            //Console.ReadKey();
        }
    }
}
