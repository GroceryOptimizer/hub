using AutoMapper;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _db;

    private readonly IMapper _map;

    public ProductService(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _map = mapper;
    }

    public async Task<List<ProductDTO>> GetAll()
    {
        var products = await _map.ProjectTo<ProductDTO>(_db.Products).ToListAsync();
        return products ?? [];
    }

    public async Task<ProductDTO?> GetById(Guid id)
    {
        var product = await _map.ProjectTo<ProductDTO>(_db.Products.Where(p => p.Id == id))
            .FirstOrDefaultAsync();
        return product;
    }
}
