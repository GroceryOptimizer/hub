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
            int placeholderVendorId = 1;

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

                //0: Loop through each StockItem in the reply from this Store
                foreach (var stockItem in shoppingReply.StockItems)
                {
                    StockItemDTO dto = new StockItemDTO(new ProductDTO(stockItem.Name), stockItem.Price);
                    //1: Check if item already exists in 'collectedReply'.
                    if (collectedReply.Values.SelectMany(list => list).Any(existingItem => existingItem.Product.Name == dto.Product.Name))
                    {
                        //2: If is - ignore the item, don't add it anywhere.
                        Console.WriteLine("Already found product " +dto.Product.Name + " in the reply-collection. Ignoring this instance.");
                        continue;
                    } 
                    //3: If not - Check if this vendor ID already exists in 'collectedReply' as a Key.
                    if (!collectedReply.ContainsKey(placeholderVendorId)){
                        //  4: If not - Add this vendor to 'collectedReply' and add the item in their value.
                        collectedReply[placeholderVendorId] = new List<StockItemDTO>{dto};
                    }
                    else
                    {
                        //  5: If is - add the item to the Key's value.
                        collectedReply[placeholderVendorId].Add(dto);
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

