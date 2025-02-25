namespace Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IStoreRepository Stores { get; }

        Task<int> CompleteAsync();
    }
}
