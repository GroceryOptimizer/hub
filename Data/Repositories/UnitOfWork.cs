using Core.Repositories;

namespace Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IStoreRepository Stores { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            this._context = context;
            Stores = new StoreRepository(context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
