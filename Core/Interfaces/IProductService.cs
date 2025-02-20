using Core.DTOs;

namespace Core.Interfaces;

public interface IProductService
{
    Task<List<ProductDTO>> GetAll();
    Task<ProductDTO?> GetById(Guid id);
}
