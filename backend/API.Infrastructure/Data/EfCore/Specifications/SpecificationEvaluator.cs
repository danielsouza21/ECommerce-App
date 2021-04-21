using System.Linq;
using API.Core.Entities;
using API.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Data.EfCore.Specifications
{
    class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            if(spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            //Adding existing Includes in the query
            query = spec.Includes.Aggregate(query, (currentQuery, expression) => currentQuery.Include(expression));

            return query;
        }
    }
}
