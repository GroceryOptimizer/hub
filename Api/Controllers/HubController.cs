using Api.Services;

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
        private readonly IConnectorService _vendorConnectorService;
        //test comment
        // Temp seed variables
        private List<StockItemDTO> stockItemsSeed = new List<StockItemDTO>
            {
                new StockItemDTO(new ProductDTO("Apple"), 2),
                new StockItemDTO(new ProductDTO("Banana"), 1),
                new StockItemDTO(new ProductDTO("Carrot"), 3),
                new StockItemDTO(new ProductDTO("Bread"), 5),
                new StockItemDTO(new ProductDTO("Milk"), 4)
            };

        public HubController(ApplicationDbContext context, IConnectorService connectorService)
        {
            this._context = context;
            this._vendorConnectorService = connectorService;
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

        // Test GET, same code as above BUT more LINQ focused
        [HttpGet("linq")]
        public async Task<ActionResult<IEnumerable<VendorVisitDTO>>> GetVendorVisitsLinq()
        {
            var dtoList = await _context.Vendors
                .Include(v => v.Coordinates) // Ensure Coordinates are loaded
                .Select(v => new VendorVisitDTO(
                    v.Id, // Assuming VendorVisitDTO requires an ID
                    new VendorDTO(
                        v.Id,
                        v.Name,
                        v.Coordinates != null ? v.Coordinates.Id : 0, // Provide the missing CoordinatesId
                        v.Coordinates != null
                            ? new CoordinatesDTO(v.Coordinates.Id, v.Coordinates.Latitude, v.Coordinates.Longitude)
                            : null // No default values
                    ),
                    new List<StockItemDTO>(stockItemsSeed) // Ensure stock items are passed
                )).ToListAsync();

            if (!dtoList.Any())
            {
                return NotFound();
            }

            return Ok(dtoList);
        }

        [HttpGet("testSendMessage")]
        public async Task<VendorVisitDTO> GetTestSendMessage()
        {
            var sendMsg = await _vendorConnectorService.SendMessageAsync("Hello from Controller");
            Console.WriteLine(sendMsg);

            return null;
        }

        [HttpGet("testGetInventory")]
        public async Task<VendorVisitDTO> GetTestInventory()
        {
            var sendInventoryRequest = await _vendorConnectorService.GetInventoryAsync();

            return null;
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
