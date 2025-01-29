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

            CreateMap<StockItem, StockItemDTO>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ReverseMap();

            CreateMap<ShoppingCart, ShoppingCartDTO>().ReverseMap();

            CreateMap<Vendor, VendorDTO>().ReverseMap();

            CreateMap<VendorVisit, VendorVisitDTO>()
                .ForMember(dest => dest.VendorId, opt => opt.MapFrom(src => src.Vendor.Id))
                .ForMember(dest => dest.VendorName, opt => opt.MapFrom(src => src.Vendor.Name))
                .ReverseMap();
        }
    }
}
