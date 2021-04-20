using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain.Entities;
using API.Domain.Interfaces;
using API.Domain.Specifications;
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

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(ISpecification<T> spec)
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
