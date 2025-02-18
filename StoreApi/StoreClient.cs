using Core.DTOs;
using Core.Entities;

using Data;

using Grpc.Core;
using Grpc.Net.Client;

using Microsoft.EntityFrameworkCore;

using StoreProto;  // The namespace we specified in the proto file

namespace StoreApi
{
    public class StoreClient
    {
        private Dictionary<string, GrpcChannel> _channels = new();
        private readonly ApplicationDbContext _context;

        public StoreClient(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<int, List<StockItemDTO>>> SendGrpcCall(ShoppingCart shoppingCart)
        {
            var storeNames = await _context.Stores
                            .Select(v => v.Name)
                            .ToListAsync();
            var storeIds = await _context.Stores
                            .Select(v => v.Id)
                            .ToListAsync();
            int index = 0;
            // Local variable to store all replies
            Dictionary<int, List<StockItemDTO>> collectedReply = new();

            foreach (var store in storeNames)
            {
                string containerName = "store_" + store.ToLower();
                string grpcAddress = $"http://{containerName}:50051";

                // Craft the connection
                if (!_channels.ContainsKey(containerName))
                {
                    _channels[containerName] = GrpcChannel.ForAddress(grpcAddress, new GrpcChannelOptions
                    {
                        Credentials = ChannelCredentials.Insecure
                    });
                }
                var channel = _channels[containerName];
                var client = new StoreService.StoreServiceClient(channel);

                // Create the InventoryRequest from the ShoppingCart entity
                var inventoryRequest = ConvertToInventoryRequest(shoppingCart);

                // Send the request
                var shoppingReply = await client.ProductsAsync(inventoryRequest);

                // Parse the request to workable DTOs and check for duplicates
                // 0: Loop through each StockItem in the reply from this Store
                foreach (var stockItem in shoppingReply.StockItems)
                {
                    StockItemDTO dto = new StockItemDTO(new ProductDTO(stockItem.Product.Name), stockItem.Price);
                    // 1: Check if item already exists in 'collectedReply'.
                    if (collectedReply.Values.SelectMany(list => list).Any(existingItem => existingItem.Product.Name == dto.Product.Name))
                    {
                        // 2: If is - ignore the item, don't add it anywhere.
                        Console.WriteLine("Already found product " + dto.Product.Name + " in the reply-collection. Ignoring this instance.");
                        continue;
                    }
                    // 3: If not - Check if this store ID already exists in 'collectedReply' as a Key.
                    if (!collectedReply.ContainsKey(storeIds[index]))
                    {
                        // 4: If not - Add this store to 'collectedReply' and add the item in their value.
                        collectedReply[storeIds[index]] = new List<StockItemDTO> { dto };
                    }
                    else
                    {
                        // 5: If is - add the item to the Key's value.
                        collectedReply[storeIds[index]].Add(dto);
                    }
                }
                index++; // Shift index by 1
            }
            return collectedReply; // return crafted reply
        }

        private InventoryRequest ConvertToInventoryRequest(ShoppingCart cart)
        {
            return new InventoryRequest
            {
                ShoppingCart = { cart.Cart.Select(p => new StoreProto.Product { Name = p.Name }) }
            };
        }
    }
}
