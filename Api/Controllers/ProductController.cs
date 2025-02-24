using Core.DTOs;

using Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            this._context = context;
        }

        // GET all products
        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetAllProducts()
        {
            // Get all stock items from stocklist
            var stockList = await _context.StockList.Include(s => s.StockItems).ThenInclude(p => p.Product).ToListAsync();

            // Make products from the stocklist
            var productList = stockList.SelectMany(s => s.StockItems).Select(p => new ProductDTO(p.Product.Name)).ToList() ?? new List<ProductDTO>();

            return Ok(productList);
        }

    }
}
