using Core.DTOs;
using Core.Entities;

using Grpc.Net.Client;

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
                    if (!stockList.Any(existingItem => existingItem.Product.Name == dto.Product.Name))
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
    }
}

