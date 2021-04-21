using System.Collections.Generic;
using System.Threading.Tasks;
using API.Core.Entities;

namespace API.Core.Interfaces
{
    public interface IStoreServices
    {
        Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<Product> GetProductById(int id);
        Task<IReadOnlyList<ProductBrand>> GetAllProductsBrands();
        Task<IReadOnlyList<ProductType>> GetAllProductsTypes();
    }
}
