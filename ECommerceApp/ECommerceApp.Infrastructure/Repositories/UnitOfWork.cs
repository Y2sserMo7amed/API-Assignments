using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Contracts;
using ECommerceApp.Infrastructure.Data;

namespace ECommerceApp.Infrastructure.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;

        
        private readonly Dictionary<string, object> _repositories = new();

        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name;

            if (_repositories.TryGetValue(typeName, out var existingRepository))
            {
                return (IGenericRepository<TEntity, TKey>)existingRepository;
            }

            var newRepository = new GenericRepository<TEntity, TKey>(_dbContext);
            _repositories[typeName] = newRepository;
            return newRepository;
        }

        public Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            return _dbContext.SaveChangesAsync(ct);
        }
    }
}
