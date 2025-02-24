using Core.DTOs;
using Core.Entities;

using Data;

using Grpc.Core;
using Grpc.Net.Client;

using Microsoft.CodeAnalysis.CSharp;
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

                //############################################## NEW FILTERING SYSTEM, SEND CLIENT EVERYTHING ##############################################
                // Parse the request to workable DTOs and check for duplicates
                foreach (var item in shoppingReply.StockItems)
                {
                    // 1: Parse to a DTO
                    StockItemDTO dto = new StockItemDTO(new ProductDTO(item.Name), item.Price);

                    // 2: Check if the vendor's ID already exists in 'collectedReply' as a Key.
                    if (!collectedReply.ContainsKey(storeIds[index]))
                    {
                        // 3: If not - Add this store to 'collectedReply' and add the item in their value.
                        collectedReply[storeIds[index]] = new List<StockItemDTO> { dto };
                    }
                    else
                    {
                        // 4: If is - add the item to the Key's value.
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
