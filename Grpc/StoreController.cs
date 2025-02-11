using Core.DTOs;
using Core.Entities;

using Grpc.Net.Client;

using Microsoft.AspNetCore.Mvc;

using StoreProto;  // The namespace we specified in the proto file

namespace HubClient
{
    public class StoreController
    {

        public static async Task<Dictionary<int, List<StockItemDTO>>> SendGrpcCall(ShoppingCart shoppingCart)
        {
            //###################### ToDo: Entire section needs to be made dynamic ######################
            var channels = new[]
            {
                "http://localhost:50051",
                "http://localhost:50052",
                "http://localhost:50053"
            };
            int placeholderVendorId = 0;

            //###########################################################################################
            //Local variable to store all replies
            Dictionary<int, List<StockItemDTO>> collectedReply = new();


            foreach (var currentChannel in channels)
            {
                //Craft the connection
                using var channel = GrpcChannel.ForAddress(currentChannel);
                var client = new StoreService.StoreServiceClient(channel);
                //Create the InventoryRequest from the ShoppingCart entity
                var inventoryRequest = ConvertToInventoryRequest(shoppingCart);
                //Send the request
                Console.WriteLine("Sending gRPC request now.");
                var shoppingReply = await client.ProductsAsync(inventoryRequest);
                //Parse the request to workable DTOs and check for duplicates
                if (!collectedReply.ContainsKey(placeholderVendorId))
                {
                    collectedReply[placeholderVendorId] = new List<StockItemDTO>();
                }
                List<StockItemDTO> stockList = collectedReply[placeholderVendorId];
                foreach (var stockItem in shoppingReply.StockItems)
                {
                    StockItemDTO dto = new StockItemDTO(new ProductDTO(stockItem.Name), stockItem.Price);
                    if(!stockList.Any(existingItem => existingItem.Product.Name == dto.Product.Name))
                    {
                        stockList.Add(dto);
                        Console.WriteLine("New item found in reply, adding to reply-collection: " + dto.Product.Name + " at cost: " + dto.Price);
                    }
                    else
                    {
                        Console.WriteLine("Already found product " + dto.Product.Name + " in reply-colletion. Ignoring this instance");
                    }
                }
                //ToDo: Remove this when section above is dynamic
                placeholderVendorId++;
            }
            //return crafted reply
            return collectedReply;
        }

        private static InventoryRequest ConvertToInventoryRequest(ShoppingCart cart)
        {
            return new InventoryRequest
            {
                ShoppingCart = { cart.Cart.Select(p => new StoreProto.Product { Name = p.Name }) }
            };
        }


        //public static async Task Connect()
        //{
        //    var channels = new[]
        //    {
        //        "http://localhost:50051",
        //        "http://localhost:50052",
        //        "http://localhost:50053"
        //    };

        //    Dictionary<int, List<StockItem>> vendorVisitReply = new Dictionary<int, List<Core.Entities.StockItem>>();
        //    int placeholderVendorId = 0;

        //    foreach (var currentChannel in channels)
        //    {
        //        // Create new channel from current index in "_channels"
        //        using var channel = GrpcChannel.ForAddress(currentChannel);
        //        // The generated client is VendorService.VendorServiceClient
        //        var client = new StoreService.StoreServiceClient(channel);

        //        Console.WriteLine("Sending gRPC request");
        //        // Setup all variables needed for the gRPC call
        //        var inventoryClient = new StoreService.StoreServiceClient(channel);
        //        var shoppingCart = new InventoryRequest();
        //        int vendorId;

        //        // Craft our testing-call to send out to Vendors                
        //        shoppingCart.ShoppingCart.Add(new Product { Name = "Milk" });
        //        shoppingCart.ShoppingCart.Add(new Product { Name = "Bread" });
        //        shoppingCart.ShoppingCart.Add(new Product { Name = "Eggs" });

        //        // Perform the call
        //        var shoppingReply = await inventoryClient.ProductsAsync(shoppingCart);

        //        // Do something with the reply of the call

        //        //List of the StockItems for this particular VendorVisit
        //        List<StockItem> stockItemList = new List<StockItem>();

        //        vendorId = placeholderVendorId;
        //        Console.WriteLine("Recieved inventory reply with {0} items from " + vendorId, shoppingReply.StockItems.Count);
        //        foreach (var item in shoppingReply.StockItems)
        //        {
        //            //Console.WriteLine(item.Name + ", costs: " + item.Price);
        //            stockItemList.Add(item);
        //        }
        //        vendorVisitReply.Add(vendorId, stockItemList);
        //        placeholderVendorId++;

        //    }
        //    Console.WriteLine("All gRPC calls done, printing list of Vendor Visits: ");
        //    foreach (int key in vendorVisitReply.Keys)
        //    {
        //        Console.WriteLine("Vendor ID " + key);
        //        foreach (StockItem item in vendorVisitReply[key])
        //        {
        //            Console.WriteLine(item.Name + ", Price: " + item.Price);
        //        }
        //    }


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

