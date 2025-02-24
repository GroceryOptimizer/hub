using System;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IStoreRepository Stores { get; }
        //IProductRepository Products { get; } // Not implemented yet

        Task<int> CompleteAsync();
    }
}
