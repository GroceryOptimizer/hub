using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.DTOs;
using Core.Entities;
using Core.Repositories;

using Data;

using Grpc.Core;
using Grpc.Net.Client;

using StoreProto;

namespace StoreApi.Services
{
    public class StoreService
    {
        private readonly Dictionary<string, GrpcChannel> _channels = new();
        private readonly IUnitOfWork _unitOfWork;

        public StoreService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Dictionary<int, List<StockItemDTO>>> SendGrpcCall(ShoppingCart shoppingCart)
        {
            var stores = await _unitOfWork.Stores.GetAllStoresAsync();
            var storeNames = stores.Select(v => v.Name).ToList();
            var storeIds = stores.Select(v => v.Id).ToList();

            int index = 0;
            // Local variable to store all replies
            Dictionary<int, List<StockItemDTO>> collectedReply = new();

            foreach (var store in stores)
            {
                var containerName = store.Name;
                var grpcAddress = $"http://{store.GrpcAddress}";
                // Craft the connection
                if (!_channels.ContainsKey(containerName))
                {
                    Console.WriteLine($"Creating channel for {containerName}");
                    _channels[containerName] = GrpcChannel.ForAddress(grpcAddress, new GrpcChannelOptions
                    {
                        Credentials = ChannelCredentials.Insecure
                    });
                }
                var channel = _channels[containerName];
                var client = new StoreProto.StoreService.StoreServiceClient(channel);

                // Create the InventoryRequest from the ShoppingCart entity
                var inventoryRequest = ConvertToInventoryRequest(shoppingCart);

                // Send the request
                var shoppingReply = await client.ProductsAsync(inventoryRequest);

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