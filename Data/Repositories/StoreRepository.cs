using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class StoreRepository : IStoreRepository
{
    private readonly ApplicationDbContext _context;

    public StoreRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<Store> Query() => _context.Stores;

    public async Task<List<Store>> GetAllStores()
    {
        return await _context.Stores.ToListAsync();
    }

    public async Task<int> Add(Store store)
    {
        _context.Stores.Add(store);
        return await _context.SaveChangesAsync();
    }
}
