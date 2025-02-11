using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/cart")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly CartService _cartService = new();

    [HttpPost]
    public async Task<ActionResult<CartRes>> FindStoresByCart([FromBody] CartReq cart)
    {
        var result = await _cartService.GetStoreByCart(cart.Items);
        return Ok(result);
    }
}
