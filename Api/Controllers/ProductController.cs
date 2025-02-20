using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/product")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _ps;

    public ProductController(IProductService productService)
    {
        _ps = productService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductDTO>>> GetAllProducts()
    {
        var products = await _ps.GetAll();
        return products.Count > 0 ? Ok(products) : NotFound();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDTO>> GetProductById(Guid id)
    {
        var product = await _ps.GetById(id);
        return product != null ? Ok(product) : NotFound();
    }
}
