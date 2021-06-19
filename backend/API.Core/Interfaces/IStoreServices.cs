using System.Collections.Generic;
using System.Threading.Tasks;
using API.Core.Entities;
using API.Core.Specifications;

namespace API.Core.Interfaces
{
    public interface IStoreServices
    {
        Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecParams productParams);
        Task<Product> GetProductById(int id);
        Task<IReadOnlyList<ProductBrand>> GetAllProductsBrands();
        Task<IReadOnlyList<ProductType>> GetAllProductsTypes();
        Task<int> GetCountAsync(ProductSpecParams productParams);
    }
}
