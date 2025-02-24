using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.Entities;
using Core.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly ApplicationDbContext _context;

        public StoreRepository(ApplicationDbContext context)
        {
            this._context = context; 
        }

        public async Task<List<Store>> GetAllStoresAsync()
        {
            return await _context.Stores.ToListAsync();
        }

        public async Task<Store?> GetStoreByIdAsync(int id)
        {
            return await _context.Stores.FindAsync(id);
        }
    }
}
