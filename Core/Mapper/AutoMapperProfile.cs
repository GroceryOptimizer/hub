using AutoMapper;

namespace Core.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Product, ProductDTO>().ReverseMap();
        CreateMap<ShoppingCart, ShoppingCartDTO>().ReverseMap();
        CreateMap<StockItem, StockItemDTO>().ReverseMap();
    }
}
