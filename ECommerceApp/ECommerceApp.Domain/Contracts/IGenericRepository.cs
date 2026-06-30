using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Specifications;

namespace ECommerceApp.Domain.Contracts
{
    
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<TEntity?> GetByIdAsync(TKey id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        
        Task<TEntity?> GetEntityWithSpecAsync(BaseSpecification<TEntity, TKey> spec);
        Task<IEnumerable<TEntity>> GetAllWithSpecAsync(BaseSpecification<TEntity, TKey> spec);
        Task<int> CountAsync(BaseSpecification<TEntity, TKey> spec);
    }
}
