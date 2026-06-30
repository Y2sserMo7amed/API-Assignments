using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Contracts;
using ECommerceApp.Domain.Specifications;
using ECommerceApp.Infrastructure.Data;
using ECommerceApp.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Infrastructure.Repositories
{
   
    internal class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _dbContext;

        public GenericRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public async Task<TEntity?> GetEntityWithSpecAsync(BaseSpecification<TEntity, TKey> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(BaseSpecification<TEntity, TKey> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> CountAsync(BaseSpecification<TEntity, TKey> spec)
        {
            
            return await ApplySpecification(spec, includePaging: false).CountAsync();
        }

        private IQueryable<TEntity> ApplySpecification(BaseSpecification<TEntity, TKey> spec, bool includePaging = true)
        {
            return SpecificationEvaluator<TEntity, TKey>.GetQuery(_dbContext.Set<TEntity>(), spec, includePaging);
        }
    }
}
