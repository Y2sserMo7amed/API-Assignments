using ECommerceApp.Domain.Common;

namespace ECommerceApp.Domain.Contracts
{
  
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
