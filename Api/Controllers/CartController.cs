using Core.DTOs;
using Core.Entities;

using Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using StoreApi;

namespace Api.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly StoreClient _storeClient;

        public CartController(ApplicationDbContext context, StoreClient storeClient)
        {
            this._context = context;
            this._storeClient = storeClient;
        }

        // POST:
        // input: Shopping Cart DTO
        // return: Collection of Vendor Visit DTOs
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VendorVisitDTO>>> PostCart(ShoppingCartDTO cart)
        {
            // Early exit if bad input
            if (cart == null || cart.Cart == null || cart.Cart.Count == 0)
            {
                return BadRequest("Cart was found empty or nonexistant.");
            }

            // Parse DTO cart to normal Products
            List<Product> products = cart.Cart.Select(p => new Product(p.Name)).ToList();
            ShoppingCart cartToSend = new ShoppingCart(products); // Create ShoppingCart with this list of Product's

            // Call the gRPC function to send the request and await response from GRPC API
            Dictionary<int, List<StockItemDTO>> result = await _storeClient.SendGrpcCall(cartToSend);

            // Make sure we get a response
            if (result == null || !result.Any())
            {
                return BadRequest("Failed to retrieve stock data from the gRPC service.");
            }

            // Get our relevant vendors ready and prepared
            var vendorIds = result.Keys.ToList();
            var relevantVendors = await _context.Vendors
                .Where(v => vendorIds.Contains(v.Id))
                .ToListAsync();

            // parse response from gRPC to VendorVisitDTOs (find vendor in db by vendor id)
            List<VendorVisitDTO> returnList = relevantVendors
                .Select(v => new VendorVisitDTO(
                    v.Id,
                    new VendorDTO(
                        v.Id,
                        v.Name,
                        new CoordinatesDTO(v.Location.Latitude, v.Location.Longitude)
                    ),
                    result.ContainsKey(v.Id) ? result[v.Id] : new List<StockItemDTO>()
                ))
                .ToList();

            return Ok(returnList);
        }
    }
}
