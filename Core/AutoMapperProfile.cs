using AutoMapper;

using Core.DTOs;
using Core.Entities;

namespace Core
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Entity → DTO
            CreateMap<Coordinates, CoordinatesDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<ShoppingCart, ShoppingCartDTO>().ReverseMap();
            CreateMap<StockItem, StockItemDTO>().ReverseMap();
            CreateMap<Vendor, VendorDTO>().ReverseMap();
            CreateMap<VendorVisit, VendorVisitDTO>().ReverseMap();
        }
    }
}
