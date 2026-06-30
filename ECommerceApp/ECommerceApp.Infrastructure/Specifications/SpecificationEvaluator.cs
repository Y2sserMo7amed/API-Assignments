using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Infrastructure.Specifications
{

    public static class SpecificationEvaluator<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public static IQueryable<TEntity> GetQuery(
            IQueryable<TEntity> query,
            BaseSpecification<TEntity, TKey> spec,
            bool includePaging = true)
        {
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            foreach (var include in spec.Includes)
            {
                query = query.Include(include);
            }

            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            
            if (includePaging && spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            return query;
        }
    }
}
