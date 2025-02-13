using Core.DTOs;
using Core.Entities;

using Data;

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
            var vendors = await _context.Vendors.ToListAsync();

            //Local variable to store all replies
            Dictionary<int, List<StockItemDTO>> collectedReply = new();

            foreach (var vendor in vendors)
            {
                //Parse the vendor address to extract the port
                var uri = new Uri("http://" + vendor.GrpcAddress);
                var port = uri.Port;
                var address = $"http://localhost:{port}";

                //Craft the connection
                if (!_channels.ContainsKey(address))
                {
                    _channels[address] = GrpcChannel.ForAddress(address);
                }

                var channel = _channels[address];
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
                        Console.WriteLine("Already found product " + dto.Product.Name + " in the reply-collection. Ignoring this instance.");
                        continue;
                    }
                    //3: If not - Check if this vendor ID already exists in 'collectedReply' as a Key.
                    if (!collectedReply.ContainsKey(vendor.Id))
                    {
                        //  4: If not - Add this vendor to 'collectedReply' and add the item in their value.
                        collectedReply[vendor.Id] = new List<StockItemDTO> { dto };
                    }
                    else
                    {
                        //  5: If is - add the item to the Key's value.
                        collectedReply[vendor.Id].Add(dto);
                    }
                }
            }
            //return crafted reply
            return collectedReply;
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

