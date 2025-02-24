using Core.DTOs;
using Core.Entities;
using Core.Repositories;
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
        private readonly IStoreRepository _storeRepository;
        private readonly StoreClient _storeClient;

        public CartController(IStoreRepository storeRepository, StoreClient storeClient)
        {
            this._storeRepository = storeRepository;
            this._storeClient = storeClient;
        }

        // POST:
        // input: Shopping Cart DTO
        // return: Collection of Store Visit DTOs
        [HttpPost]
        public async Task<ActionResult<IEnumerable<StoreVisitDTO>>> PostCart(ShoppingCartDTO cart)
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

            // Get our relevant Stores ready and prepared
            var storeIds = result.Keys.ToList();
            var relevantStores = await _storeRepository.GetAllStoresAsync();
            var filteredStores = relevantStores.Where(v => storeIds.Contains(v.Id)).ToList();

            // parse response from gRPC to StoreVisitDTOs (find Store in db by Store id)
            List<StoreVisitDTO> returnList = filteredStores
                .Select(v => new StoreVisitDTO(
                    v.Id,
                    new StoreDTO(
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
