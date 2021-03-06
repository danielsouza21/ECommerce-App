using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Core.Entities;
using API.Core.Interfaces;
using API.Core.Specifications;
using API.Infrastructure.Data.EfCore.Specifications;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Data.EfCore
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<int> CountWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetEntityWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> GetWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }


        #region private methods
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            var entityAsQuery = _context.Set<T>().AsQueryable();
            return SpecificationEvaluator<T>.GetQuery(entityAsQuery, spec);
        }
        #endregion
    }
}
