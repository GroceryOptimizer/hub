using AutoMapper;

namespace Core.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Entity → DTO
        CreateMap<Coordinates, CoordinatesDTO>()
            .ReverseMap();
        CreateMap<Product, ProductDTO>().ReverseMap();
        CreateMap<ShoppingCart, ShoppingCartDTO>().ReverseMap();
        CreateMap<StockItem, StockItemDTO>().ReverseMap();
    }
}
