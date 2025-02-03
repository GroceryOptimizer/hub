using Core.DTOs;

using Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/hub")]
    [ApiController]
    public class HubController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Temp seed variables
        private List<StockItemDTO> stockItemsSeed = new List<StockItemDTO>
            {
                new StockItemDTO(new ProductDTO("Apple"), 2),
                new StockItemDTO(new ProductDTO("Banana"), 1),
                new StockItemDTO(new ProductDTO("Carrot"), 3),
                new StockItemDTO(new ProductDTO("Bread"), 5),
                new StockItemDTO(new ProductDTO("Milk"), 4)
            };

        public HubController(ApplicationDbContext context)
        {
            this._context = context;
        }

        // Test GET function that returns VendorVisits from all Vendors in the db
        [HttpGet]
        public async Task<IEnumerable<VendorVisitDTO>> GetVendorVisitsAsync()
        {
            // Fetch Vendors and include related Coordinates
            var vendors = await _context.Vendors
                .Include(v => v.Coordinates)
                .ToListAsync();

            // Map Vendors to DTOs
            var vendorVisits = vendors.Select(v =>
            new VendorVisitDTO(
                v.Id,
                new VendorDTO
                (
                    v.Id,
                    v.Name,
                    v.Coordinates.Id,
                    new CoordinatesDTO
                    (
                        v.Coordinates.Id,
                        v.Coordinates.Latitude,
                        v.Coordinates.Longitude
                    )
                ),
                new List<StockItemDTO>(stockItemsSeed)
            )).ToList();

            return vendorVisits;
        }

        // POST:
        // input: Shopping Cart DTO
        // return: Collection of Vendor Visit DTOs
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VendorVisitDTO>>> PostCart(ShoppingCartDTO cart)
        {
            // todo: early exit if bad input
            // pass cart to GRPC API call
            // await response from GRPC API
            // parse response to VendorVisitDTOs (find vendor in db by vendor id)
            // create VendorVisitDTO list to return

            return null;
        }
    }
}
