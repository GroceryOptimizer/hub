using Core.DTOs;
using Core.Entities;

using Data;

using HubClient;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/hub")]
    [ApiController]
    public class HubController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HubController(ApplicationDbContext context)
        {
            this._context = context;
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
            // pass cart to GRPC API call
            //Parse DTO cart to normal Products
            List<Product> products = cart.Cart.Select(p => new Product(p.Name)).ToList();
            //Create ShoppingCart with this list of Product's
            ShoppingCart cartToSend = new ShoppingCart(products);
            //Call the gRPC function to send the request and await response from GRPC API
            Dictionary<int, List<StockItemDTO>> result = await StoreController.SendGrpcCall(cartToSend, _context);
            // get our relevant vendors ready and prepared
            var vendorIds = result.Keys.ToList();
            var relevantVendors = await _context.Vendors
                                    .Include(v => v.Coordinates)
                                    .Where(v => vendorIds.Contains(v.Id))
                                    .ToListAsync();
            // parse response from gRPC to VendorVisitDTOs (find vendor in db by vendor id)
            List<VendorVisitDTO> returnList = relevantVendors.Select(v =>
                new VendorVisitDTO(
                    v.Id,
                    new VendorDTO(
                        v.Id,
                        v.Name,
                        v.CoordinatesId,
                            new CoordinatesDTO(
                                v.Coordinates.Id,
                                v.Coordinates.Latitude,
                                v.Coordinates.Longitude
                            )
                        ),
                    result.ContainsKey(v.Id) ? result[v.Id] : new List<StockItemDTO>()
                    )
                ).ToList();

            return Ok(returnList);
        }
    }
}
