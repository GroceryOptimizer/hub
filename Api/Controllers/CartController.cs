using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/cart")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpPost]
    public async Task<ActionResult<CartRes>> FindStoresByCart([FromBody] CartReq cart)
    {
        var result = await _cartService.GetStoreByCart(cart.Items);
        return Ok(result);
    }
}
