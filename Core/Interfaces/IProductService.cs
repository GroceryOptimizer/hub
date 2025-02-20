using Core.Entities;

namespace Core.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetAll();
}
