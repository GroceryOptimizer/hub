using Core.DTOs;
using Core.Entities;

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
        private List<StockItem> stockItemsSeed = new List<StockItem>
            {
                new StockItem(new Product("Apple"), 2),
                new StockItem(new Product("Banana"), 1),
                new StockItem(new Product("Carrot"), 3),
                new StockItem(new Product("Bread"), 5),
                new StockItem(new Product("Milk"), 4)
            };

        private List<VendorVisit> vendorVisitsSeed = new List<VendorVisit>
        {
        new VendorVisit(
            new Vendor { Name = "Fresh Market", CoordinatesId = 1 },
            new List<StockItem>
            {
                new StockItem(new Product("Apple"), 2),
                new StockItem(new Product("Banana"), 1)
            }
        ),
        new VendorVisit(
            new Vendor { Name = "Organic Foods", CoordinatesId = 2 },
            new List<StockItem>
            {
                new StockItem(new Product("Carrot"), 3),
                new StockItem(new Product("Lettuce"), 2)
            }
        ),
        new VendorVisit(
            new Vendor { Name = "Healthy Bites", CoordinatesId = 3 },
            new List<StockItem>
            {
                new StockItem(new Product("Milk"), 4),
                new StockItem(new Product("Yogurt"), 5)
            }
        )
        };

        public HubController(ApplicationDbContext context)
        {
            this._context = context;
        }

        // GET: skriv endpoint här (t.ex. api/movies)
        [HttpGet]
        public async Task<IEnumerable<VendorVisitDTO>> GetVendorVisitsAsync()
        {
            // Fetch Vendors and include related Coordinates
            var vendors = await _context.Vendors
                .Include(v => v.Coordinates)
                .ToListAsync();

            // Map Vendors to DTOs
            var vendorVisits = vendors.Select(v => new VendorVisitDTO
            {
                VendorId = v.Id,
                Vendor = new VendorDTO
                {
                    Id = v.Id,
                    Name = v.Name,
                    CoordinatesId = v.Coordinates.Id,
                    Coordinates = new CoordinatesDTO
                    {
                        Id = v.Coordinates.Id,
                        Latitude = v.Coordinates.Latitude,
                        Longitude = v.Coordinates.Longitude
                    }
                },
                StockItems = new List<StockItemDTO>()
            }).ToList();

            return vendorVisits;
        }

        // POST:
        // input: Shopping Cart DTO
        // return: Collection of Vendor Visit DTOs
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VendorVisitDTO>>> PostCart(ShoppingCartDTO cart)
        {
            // todo: early exit if bad input
            return null;
        }
    }
}
