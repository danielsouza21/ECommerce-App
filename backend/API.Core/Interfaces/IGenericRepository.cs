using System.Collections.Generic;
using System.Threading.Tasks;
using API.Core.Entities;
using API.Core.Specifications;

namespace API.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GetWithSpecAsync(ISpecification<T> spec);
    }
}
